using ArmyVkAPI.Interfaces;
using ArmyVkAPI.Realisations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VkNet;

namespace ArmyVkAPI
{
    public class MyApiVK
    {
        #region Свойства

        private VkApi token; // Токен
        public IAuthorization AuthLogic;
        UsersLogic logic;

        #endregion

        public MyApiVK()
        {
            AuthLogic = new AuthorizationLogic();

            AuthLogic.AuthSuccessful += AuthLogic_AuthSuccessful;
        }

        /// <summary>
        /// Подписываемся на событие после авторизации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AuthLogic_AuthSuccessful(object sender, EventArgs e)
        {
            logic = new UsersLogic(token);
        }
    }
}
