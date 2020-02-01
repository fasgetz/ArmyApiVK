using ArmyVkAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

            List<User> list = new List<User>();

            // Получаем список иностранных друзей
            foreach (var user in token.Friends.Get(new VkNet.Model.RequestParams.FriendsGetParams()
            {
                UserId = VK_ID,
                Count = 10000,
                Fields = ProfileFields.All
            }).Where(i => i.Country != null && i.Country?.Id != 1).ToArray())
            {
                list.Add(user);
            }

            return list.ToArray();
        }

        /// <summary>
        /// Получить список друзей пользователя
        /// </summary>
        /// <param name="VK_ID">Айди пользователя ВК</param>
        /// /// <param name="allData">Полная информация</param>
        /// <returns>Массив друзей</returns>
        public User[] GetFriendsUser(int VK_ID, bool allData = true)
        {

            // Формируем запрос
            var request = new VkNet.Model.RequestParams.FriendsGetParams()
            {
                UserId = VK_ID,
                Count = 10000,
            };

            switch (allData)
            {
                // Если true, то вернуть всю информацию
                case (true):
                    request.Fields = ProfileFields.All;
                    break;
                case (false):
                    //request.Fields = ProfileFields.;
                    break;
                default:
                    break;
            }

            // Вернуть полный список друзей пользователя
            return token.Friends.Get(request).ToArray();
        }

        /// <summary>
        /// Получить информацию о пользователе
        /// </summary>
        /// <param name="VK_ID">Айди пользователя ВК</param>
        /// <returns>Возвращает информацию о пользователе</returns>
        public User GetUser(int VK_ID)
        {

            return token.Users.Get(new long[]
            {
                VK_ID
            }, VkNet.Enums.Filters.ProfileFields.All).FirstOrDefault();
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


            List<User> usersHaveUS = new List<User>();

            // Теперь проходимся по массиву и делаем список людей, которые имеют В/Ч на странице
            Parallel.For(0, friends.Length / 10, (int i) =>
            {
                for (int s = i * 10; s < (i + 1) * 10; s++)
                {
                    try
                    {
                        // Инфа о юзере
                        var user = token.Users.Get(new long[]
                        {
                                friends[s].Id
                        }, VkNet.Enums.Filters.ProfileFields.All).FirstOrDefault();

                        // Если такого юзера нашли, то добавь его в список пользователей, которые имеют В/Ч
                        if (user.Military != null)
                        {
                            usersHaveUS.Add(user);
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.WriteLine($"{user.Id}) {user.FirstName} {user.LastName} - {user.Country?.Title} имеет вч");
                        }
                            
                        // Иначе можно добавить в список пользователей, которые не имеют В/Ч
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.WriteLine($"{user.Id}) {user.FirstName} {user.LastName} - {user.Country?.Title} не имеет вч");
                        }
                    }
                    catch (Exception)
                    {
                        
                    }

                        


                }
            });

            return usersHaveUS.ToArray();

        }



        /// <summary>
        /// Вторая версия метода, в параметры которого, по ссылке, передаются коллекции для формирования в реальном времени результатов
        /// </summary>
        /// <param name="VK_ID">Айди пользователя ВК</param>
        /// <param name="UsersHasUS">Список пользователей, которые имеют В/Ч на странице</param>
        /// <param name="UsersDontHasUS">Список пользователей, которые не имеют В/Ч на странице</param>
        /// <returns>Возвращает true, если поиск закончен, иначе false</returns>
        public ICollection<User> GetUserHasFriendsUS(int VK_ID, ICollection<User> UsersHasUS, ICollection<User> UsersDontHasUS)
        {
            try
            {
                // Получаем полный список друзей
                var friends = token.Friends.Get(new VkNet.Model.RequestParams.FriendsGetParams()
                {
                    UserId = VK_ID,
                    Count = 10000,
                    Fields = ProfileFields.Military // Настройка
                }).ToArray();




                // Теперь проходимся по массиву и делаем список людей, которые имеют В/Ч на странице
                Parallel.For(0, friends.Length / 10, (int i) =>
                {
                    for (int s = i * 10; s < (i + 1) * 10; s++)
                    {
                        try
                        {
                            // Инфа о юзере
                            var user = token.Users.Get(new long[]
                            {
                                friends[s].Id
                            }, VkNet.Enums.Filters.ProfileFields.All).FirstOrDefault();

                            // Если такого юзера нашли, то добавь его в список пользователей, которые имеют В/Ч
                            if (user.Military != null)
                            {
                                UsersHasUS.Add(new User());
                            }
                            // Иначе можно добавить в список пользователей, которые не имеют В/Ч
                            else
                                UsersHasUS.Add(new User());


                        }
                        catch (Exception)
                        {

                        }




                    }
                });


            }
            catch (Exception)
            {
                // Если случилась какая-то ошибка, то верни false
                return null;
            }

            return UsersHasUS;

        }

        /// <summary>
        /// Имеет ли пользователь воинскую часть
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>Возвращает true, если имеет, иначе false</returns>
        public bool UserHasUnitSoldier(User user)
        {
            // Инфа о юзере
            var findUser = token.Users.Get(new long[]
            {
                user.Id
            }, VkNet.Enums.Filters.ProfileFields.Military).FirstOrDefault();


            // Если пользователь имеет В/Ч, то вернуть true, иначе false
            return findUser.Military != null ? true : false;
        }
    }
}
