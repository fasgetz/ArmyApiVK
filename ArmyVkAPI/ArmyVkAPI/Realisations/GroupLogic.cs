using ArmyVkAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace ArmyVkAPI.Realisations
{
    public class GroupLogic : IGroupLogic
    {
        private VkApi token;

        public GroupLogic(VkApi token)
        {
            this.token = token;
        }

        /// <summary>
        /// Возвращает массив групп пользователя
        /// </summary>
        /// <param name="VkUserId">Айди пользователя ВК</param>
        /// <returns>Возвращает группы пользователя ВК</returns>
        public Group[] GetGroups(int VkUserId)
        {
            return token.Groups.Get(new GroupsGetParams()
            {
                UserId = VkUserId,
                Count = 1000,
                Extended = true,
                Fields = GroupsFields.All
            }).ToArray();
        }
    }
}
