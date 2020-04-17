using ArmyVkAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;

namespace ArmyVkAPI.Realisations
{
    public class AudiousLogic : IAudiousLogic
    {
        private VkApi token;

        public AudiousLogic(VkApi token)
        {
            this.token = token;
        }

        /// <summary>
        /// Возвращает массив аудиозаписей пользователя
        /// </summary>
        /// <param name="VkUserId">Айди пользователя ВК</param>
        /// <returns>Возвращает аудиозаписи пользователя ВК</returns>
        /// 
        public Audio[] GetAudious(int VkUserId)
        {
            var audious = token.Audio.Get(new AudioGetParams()
            {
                OwnerId = VkUserId,
                Count = 6000
            }).ToArray();

            return audious;
        }
    }
}

