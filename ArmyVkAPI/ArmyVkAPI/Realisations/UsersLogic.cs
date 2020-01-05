using ArmyVkAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;

namespace ArmyVkAPI.Realisations
{
    /// <summary>
    /// Логика работы с пользователями
    /// </summary>
    /// 

    public class UsersLogic : IUserLogic
    {
        private VkApi token;

        public UsersLogic(VkApi token)
        {
            this.token = token;
        }

        /// <summary>
        /// Получить иностранных друзей пользователя ВК
        /// </summary>
        /// /// <param name="VK_ID">Айди пользователя ВК</param>
        /// <returns>Массив иностранных друзей</returns>
        public User[] GetForeignFriends(int VK_ID)
        {
            // Получаем список иностранных друзей
            return token.Friends.Get(new VkNet.Model.RequestParams.FriendsGetParams()
            {
                UserId = VK_ID,
                Count = 10000,
                Fields = ProfileFields.All
            }).Where(i => i.Country != null && i.Country?.Id != 1).ToArray();

            // 

        }

        /// <summary>
        /// Получить информацию о пользователе
        /// </summary>
        /// <param name="VK_ID">Айди пользователя ВК</param>
        /// <returns>Возвращает информацию о пользователе</returns>
        public User GetUser(int VK_ID)
        {
            var user = token.Users.Get(new long[]
            {
                VK_ID
            }, VkNet.Enums.Filters.ProfileFields.All).FirstOrDefault();


            return user;
        }


        /// <summary>
        /// (Многопоточная версия) Получить друзей пользователя, у которых есть воинские части на странице
        /// </summary>
        /// <param name="VK_ID">Айди пользователя</param>
        /// <returns>Массив друзей, у которых указана В/Ч</returns>
        public User[] GetUserHasFriendsUS(int VK_ID)
        {
            // Получаем полный список друзей
            var friends = token.Friends.Get(new VkNet.Model.RequestParams.FriendsGetParams()
            {
                UserId = VK_ID,
                Count = 10000,
                Fields = ProfileFields.Military // Настройка
            }).ToArray();


            List<User> users = new List<User>();

            // Теперь проходимся по массиву и делаем список людей, которые имеют В/Ч на странице
            Parallel.For(0, friends.Length / 10, (int i) =>
            {
                for (int s = i * 10; s < (i + 1) * 10; s++)
                {
                    // Инфа о юзере
                    var user = token.Users.Get(new long[]
                    {
                                friends[s].Id
                    }, VkNet.Enums.Filters.ProfileFields.All).FirstOrDefault();

                    // Если такого юзера нашли, то добавь его в список
                    if (user.Military != null)
                    {
                        Console.WriteLine($"{user.Id} {user.FirstName} {user.LastName} {user.Country?.Title} {user.City?.Title} {user.Military?.Unit}");
                        users.Add(user);
                    }
                    else
                        Console.WriteLine($"Не имеет В/Ч {user.Id} {user.FirstName} {user.LastName}");

                }
            });

            return users.ToArray();

        }
    }
}
