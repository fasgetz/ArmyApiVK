using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet.Model.Attachments;

namespace ArmyVkAPI.Interfaces
{
    public interface IAudiousLogic
    {
        /// <summary>
        /// Возвращает массив аудиозаписей пользователя
        /// </summary>
        /// <param name="VkUserId">Айди пользователя ВК</param>
        /// <returns>Возвращает аудиозаписи пользователя ВК</returns>
        Audio[] GetAudious(int VkUserId);
    }
}
