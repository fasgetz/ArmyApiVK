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
    }
}
