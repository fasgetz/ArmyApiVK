using ArmyVkAPI.Events;
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
        private IAuthorization AuthLogic; // Логика авторизации
        public IUserLogic UserLogic;

        #endregion

        public MyApiVK()
        {
            AuthLogic = new AuthorizationLogic();

            // Подписываемся на событие для успешной авторизации
            AuthLogic.AuthSuccessful += AuthLogic_AuthSuccessful;
        }


        #region События

        /// <summary>
        /// Выполняем метод, если авторизация успешна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AuthLogic_AuthSuccessful(object sender, MyEventArgs e)
        {
            // Выделяем память остальной логике для работы с сервисом ВК
            token = e.GetToken();
            UserLogic = new UsersLogic(token);    
        }

        #endregion

        // Метод авторизации
        public bool Authorization(string login, string password)
        {
            token = AuthLogic.AuthorizationUser(login, password);

            return token != null ? true : false;
        }
    }
}
