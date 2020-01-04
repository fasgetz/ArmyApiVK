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
    }
}
