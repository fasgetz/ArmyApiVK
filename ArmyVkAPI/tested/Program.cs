using ArmyVkAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tested
{
    class Program
    {
        static void Main()
        {
            MyApiVK api = new MyApiVK();
            var auth = api.Authorization("89114876557", "Simplepass19");
            Console.WriteLine(auth);



            var users = api.UserLogic.GetForeignFriends(97247755);
            
            foreach (var user in users)
            {
                Console.WriteLine($"https://vk.com/id{user.Id} {user.FirstName} {user.LastName} {user.Country?.Title} {user.City?.Title} {user.Military?.Unit}");
            }


            Console.ReadKey();
        }
    }
}
