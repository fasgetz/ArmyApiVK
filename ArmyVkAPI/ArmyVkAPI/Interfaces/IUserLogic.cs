using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyVkAPI.Interfaces
{
    public interface IUserLogic
    {

        /// <summary>
        /// Получить иностранных друзей пользователя ВК
        /// </summary>
        /// /// <param name="VK_ID">Айди пользователя ВК</param>
        /// <returns>Массив иностранных друзей</returns>
        VkNet.Model.User[] GetForeignFriends(int VK_ID);

        /// <summary>
        /// (Многопоточная версия) Получить друзей пользователя, у которых есть воинские части на странице
        /// </summary>
        /// <param name="VK_ID">Айди пользователя</param>
        /// <returns>Массив друзей, у которых указана В/Ч</returns>
        VkNet.Model.User[] GetUserHasFriendsUS(int VK_ID);


        /// <summary>
        /// Получить информацию о пользователе
        /// </summary>
        /// <param name="VK_ID">Айди пользователя ВК</param>
        /// <returns>Возвращает информацию о пользователе</returns>
        VkNet.Model.User GetUser(int VK_ID);
    }
}
