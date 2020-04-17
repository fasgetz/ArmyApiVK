using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet.Model;

namespace ArmyVkAPI.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с группами ВК
    /// </summary>
    public interface IGroupLogic
    {

        /// <summary>
        /// Возвращает массив групп пользователя
        /// </summary>
        /// <param name="VkUserId">Айди пользователя ВК</param>
        /// <returns>Возвращает группы пользователя ВК</returns>
        Group[] GetGroups(int VkUserId);
    }
}
