using System.Collections.Generic;
using VkNet.Model;

namespace ArmyVkAPI.Interfaces
{
    public interface IUserLogic
    {

        /// <summary>
        /// Получить иностранных друзей пользователя ВК
        /// </summary>
        /// /// <param name="VK_ID">Айди пользователя ВК</param>
        /// <returns>Массив иностранных друзей</returns>
        User[] GetForeignFriends(int VK_ID, int Foreign = 1);


        /// <summary>
        /// Получить список друзей пользователя
        /// </summary>
        /// <param name="VK_ID">Айди пользователя ВК</param>
        /// <returns>Массив друзей</returns>
        User[] GetFriendsUser(int VK_ID, bool allData = true);

        /// <summary>
        /// Имеет ли пользователь воинскую часть
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>Возвращает true, если имеет, иначе false</returns>
        bool UserHasUnitSoldier(User user);

        /// <summary>
        /// (Многопоточная версия) Получить друзей пользователя, у которых есть воинские части на странице
        /// </summary>
        /// <param name="VK_ID">Айди пользователя</param>
        /// <returns>Массив друзей, у которых указана В/Ч</returns>
        User[] GetUserHasFriendsUS(int VK_ID);


        /// <summary>
        /// Вторая версия метода, в параметры которого, по ссылке, передаются коллекции для формирования в реальном времени результатов
        /// </summary>
        /// <param name="VK_ID">Айди пользователя ВК</param>
        /// <param name="UsersHasUS">Список пользователей, которые имеют В/Ч на странице</param>
        /// <param name="UsersDontHasUS">Список пользователей, которые не имеют В/Ч на странице</param>
        /// <returns>Возвращает true, если поиск закончен, иначе false</returns>
        ICollection<User> GetUserHasFriendsUS(int VK_ID,ICollection<User> UsersHasUS, ICollection<User> UsersDontHasUS);

        /// <summary>
        /// Получить информацию о пользователе
        /// </summary>
        /// <param name="VK_ID">Айди пользователя ВК</param>
        /// <returns>Возвращает информацию о пользователе</returns>
        User GetUser(int VK_ID);
    }
}
