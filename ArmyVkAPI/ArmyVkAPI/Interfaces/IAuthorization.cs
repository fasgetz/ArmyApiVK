using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;

namespace ArmyVkAPI.Interfaces
{
    public interface IAuthorization
    {
        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="login">логин</param>
        /// <param name="password">пароль</param>
        /// <returns>Уникальный токен для работы с сервисом</returns>
        VkApi AuthorizationUser(string login, string password);


        event EventHandler AuthSuccessful;
    }
}
