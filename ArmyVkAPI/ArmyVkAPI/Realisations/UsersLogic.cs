using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;

namespace ArmyVkAPI.Realisations
{
    /// <summary>
    /// Логика работы с пользователями
    /// </summary>
    /// 

    class UsersLogic
    {
        private VkApi token;

        public UsersLogic(VkApi token)
        {
            this.token = token;
        }
    }
}
