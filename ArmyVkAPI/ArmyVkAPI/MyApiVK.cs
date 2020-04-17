using ArmyVkAPI.Events;
using ArmyVkAPI.Interfaces;
using ArmyVkAPI.Realisations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.AudioBypassService.Extensions;
using VkNet.Enums.Filters;
using VkNet.Model;

namespace ArmyVkAPI
{
    public class MyApiVK
    {
        #region Свойства

        public event EventHandler<MyEventArgs> AuthSuccessful;
        private VkApi token; // Токен
        public IUserLogic UserLogic { get; private set; }
        public IGroupLogic GroupLogic { get; private set; }

        #endregion

        public MyApiVK()
        {
            //AuthLogic = new AuthorizationLogic();

            // Подписываемся на событие для успешной авторизации
            AuthSuccessful += AuthLogic_AuthSuccessful;
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
            //token = e.GetToken();
            UserLogic = new UsersLogic(token);
            GroupLogic = new GroupLogic(token);
        }

        #endregion

        // Метод авторизации
        public bool Authorization(string login, string password)
        {
            var services = new ServiceCollection();
            services.AddAudioBypass();
            token = new VkApi(services);
            token.Authorize(new ApiAuthParams
            {
                ApplicationId = 123456,
                Login = login,
                Password = password,
                Settings = Settings.All
            });
            //token = AuthLogic.AuthorizationUser(login, password);

            // Если авторизация успешна, то сообщи об этом подписчикам
            if (token.IsAuthorized)
                AuthSuccessful?.Invoke(this, new MyEventArgs(token));

            return token.IsAuthorized;
        }

        /// <summary>
        /// Авторизован ли пользователь
        /// </summary>
        /// <returns>True, если пользователь авторизован</returns>
        public bool IsAuth()
        {
            //if (token == null)
            //    return false;

            return token.IsAuthorized;
        }
    }
}
