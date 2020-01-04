using ArmyVkAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;

namespace ArmyVkAPI.Realisations
{
    class AuthorizationLogic : IAuthorization
    {
        public event EventHandler AuthSuccessful;

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="login">логин</param>
        /// <param name="password">пароль</param>
        /// <returns>Уникальный токен для работы с сервисом</returns>
        public VkApi AuthorizationUser(string login, string password)
        {
            var api = new VkApi();

            api.Authorize(new ApiAuthParams
            {
                ApplicationId = 123456,
                Login = login,
                Password = password,
                Settings = Settings.All
            });

            AuthSuccessful?.Invoke(this, new EventArgs());

            return api;
        }
    }
}
