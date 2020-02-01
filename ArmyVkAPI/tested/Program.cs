using ArmyVkAPI;
using System;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;

namespace tested
{
    class Program
    {
        static void Main()
        {



            //var user = api.Users.Get(new long[]
            //            {
            //    189251970
            //            }, VkNet.Enums.Filters.ProfileFields.All).FirstOrDefault();



            //Console.WriteLine($"{user.LastSeen.Time} {user.City?.Title} {user.Connections?.Skype}");

            //var groups = api.Groups.Get(new VkNet.Model.RequestParams.GroupsGetParams() { UserId = 189251970, Extended = true });
            //foreach (var g in groups)
            //{
            //    Console.WriteLine(g.Name);
            //}



            MyApiVK api = new MyApiVK();
            var auth = api.Authorization("89114876557", "Simplepass19");
            //api.UserLogic.GetUserHasFriendsUS(59204972);

            var users = api.UserLogic.GetFriendsUser(209480956, true);

            // Перебираем массив чтобы узнать есть ли у пользователя В/Ч
            foreach (var item in users)
            {
                var resoult = api.UserLogic.UserHasUnitSoldier(item);

                if (resoult == false)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Пользователь {item.Id} - {item.FirstName} {item.LastName} не имеет В/Ч");
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Пользователь {item.Id} - {item.FirstName} {item.LastName} имеет В/Ч");
                }
            }

            Console.WriteLine("end");

            Console.ReadKey();
        }
    }
}
