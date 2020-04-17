using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using tested.DataBases;
using tested.Models;
using tested.NewModels;
using tested.shtat;
using tested.VkUsers;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;
using Word = Microsoft.Office.Interop.Word;
using VkNet.AudioBypassService.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace tested
{
    class Program
    {


        /// <summary>
        /// Создает отчет по юзерам вк (которые содержат экстремистский материал)
        /// </summary>
        private static void FromDoykovWordUsersExtremist()
        {
            using (NewDB db = new NewDB())
            {
                var api = new VkApi();

                api.Authorize(new ApiAuthParams
                {
                    ApplicationId = 123456,
                    Login = "89114876557",
                    Password = "Simplepass19",
                    Settings = Settings.All
                });



                List<User> users = new List<User>();

                foreach (var item in db.ZigHailVkUsers)
                {
                    
                    var vkuser = api.Users.Get(new long[]
                        {
                            (int)item.VkIdUser
                        }, VkNet.Enums.Filters.ProfileFields.All).FirstOrDefault();


                    if (vkuser != null)
                    {

                        string word = $"{vkuser.FirstName} {vkuser.LastName}";


                        // Если есть дата рождения то добавить
                        if (vkuser.BirthDate != null)
                            word += $", {vkuser.BirthDate}";


                        // Если есть город, то добавить
                        if (vkuser.City != null)
                            word += $", г. {vkuser.City.Title}.";
                        else
                            word += ".";



                        if (vkuser.Education != null)
                        {

                            switch (vkuser.Sex)
                            {
                                case (VkNet.Enums.Sex.Female):
                                    word += " Окончила ";
                                    break;
                                case (VkNet.Enums.Sex.Male):
                                    word += " Окончил ";
                                    break;
                                default:
                                    break;

                            }

                            word += $"{vkuser.Education.UniversityName}.";
                        }


                        switch (vkuser.Sex)
                        {
                            case (VkNet.Enums.Sex.Female):
                                word += " Зарегистрирована";
                                break;
                            case (VkNet.Enums.Sex.Male):
                                word += " Зарегистрирован";
                                break;
                            default:
                                break;

                        }

                        word += $" в социальной сети «Вконтакте», адрес: vk.com/id{vkuser.Id};";

                        Console.WriteLine($"{word}");
                        //users.Add(vkuser);
                    }
                }
            }


        }

        private static void FromDoykovWord()
        {
            List<FoundMaterials> items = new List<FoundMaterials>();
            using (MyDB db = new MyDB())
            {
                items = db.FoundMaterials.Where(s => s.IdMaterial == 4984 && s.DateOfEntry >= new DateTime(2020, 2, 14)).ToList();


                //// Создаём объект word
                //Microsoft.Office.Interop.Word._Application OneWord = new Microsoft.Office.Interop.Word.Application();

                //// Создаем документ
                //var OneDoc = OneWord.Documents.Add();

                //int i = 0;
                //foreach (var item in items)
                //{
                //    // Блок источников
                //    OneDoc.Content.InsertAfter($"Аудиозапись «Сирия - очень трогательный нашид!» продолжительностью 3 минуты 07 секунды, {item.WebAddress}, решение Ленинского районного суда г. Ульяновска от 13.05.2019;\n");

                //    // Блок приложений
                //}

                //// Выходим и закрываем
                //OneDoc.SaveAs2(@"D:\Источники2.docx");
                //OneDoc.Close();
                //OneWord.Quit();
            }


            // Анонимный метод создания приложений
            {
                // Создаём объект word
                Microsoft.Office.Interop.Word._Application OneWord = new Microsoft.Office.Interop.Word.Application();

                // Создаем документ
                var OneDoc = OneWord.Documents.Add();

                // Параметры (проверить позже)
                int i = 0;
                object f = false;
                object t = true;
                object range = Type.Missing;

                foreach (var item in items)
                {
                    // Первым делом необходимо сохранить картинку из byte[] в .png
                    using (System.Drawing.Image image = System.Drawing.Image.FromStream(new MemoryStream(item.ScreenShot)))
                    {
                        image.Save(@"D:\user.png", ImageFormat.Png);  // Or Png
                    }

                    ++i;
                    OneWord.ActiveDocument.Characters.Last.Select();
                    
                    
                    
                    OneWord.Selection.Collapse();
                    OneDoc.Content.InsertAfter($"Приложение {i}");
                    OneWord.ActiveDocument.Characters.Last.Select();
                    OneWord.Selection.Collapse();
                    OneDoc.InlineShapes.AddPicture(@"D:\user.png", ref f, ref t, ref range);
                    


                    if (i % 2 == 0)
                    {
                        OneWord.ActiveDocument.Characters.Last.Select();
                        OneWord.Selection.Collapse();

                        OneWord.Selection.InsertBreak(Microsoft.Office.Interop.Word.WdBreakType.wdPageBreak);
                    }
                }

                // Выходим и закрываем
                OneDoc.SaveAs2(@"D:\Приложения2.docx");
                OneDoc.Close();
                OneWord.Quit();
            }
        }

        static void FindUsersFromNameFamilyDateOfBirth()
        {
            List<users> searchUserList;
            using (MyDB db = new MyDB())
            {
                // получаем список искомых пользователей
                searchUserList = db.users.Where(i => i.IsFinded != true && i.DateBirth != null && i.id > 113).ToList();

            }

            var api = new VkApi();

            api.Authorize(new ApiAuthParams
            {
                ApplicationId = 123456,
                Login = "89114876557",
                Password = "Simplepass19",
                Settings = Settings.All
            });

            // Далее проходимся по юзерам

            foreach (var findUser in searchUserList)
            {

                // Ищем юзера в вк
                var searchUsers = api.Users.Search(new UserSearchParams()
                {
                    Query = $"{findUser.Name} {findUser.Family}",
                    Count = 1000,
                    //City = 61,
                    BirthDay = (ushort?)findUser.DateBirth.Value.Day,
                    BirthMonth = (ushort?)findUser.DateBirth.Value.Month,
                    BirthYear = (ushort?)findUser.DateBirth.Value.Year,
                }).ToList();


                // Если есть результаты, то добавить их в бд
                if (searchUsers.Count != 0)
                {
                    using (MyDB db = new MyDB())
                    {
                        var userdb = db.users.FirstOrDefault(i => i.id == findUser.id);

                        userdb.IsFinded = true;

                        foreach (var item in searchUsers)
                        {
                            userdb.SocialNetworkUsers.Add(new SocialNetworkUsers()
                            {
                                WebAddress = $"https://vk.com/id{item.Id}"
                            });

                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.WriteLine($"finded: {findUser.id}) {item.FirstName} {item.LastName} - https://vk.com/id{item.Id}");
                        }

                        db.SaveChanges();
                    }
                }
                // Иначе вывести на экран о неудаче
                else
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{findUser.id}) {findUser.Name} {findUser.Family} - не найден");
                }

            }
        }

        //static void CanUserHaveUnitSoldierVK()
        //{
        //    MyApiVK api = new MyApiVK();
        //    api.Authorization("89114876557", "Simplepass19");

        //    IdUsers[] users;

        //    using (TestModel db = new TestModel())
        //    {
        //        users = db.IdUsers.ToArray();


        //        // Использовать многопоточную версию
        //        Parallel.For(0, users.Length / 100, (int i) =>
        //        {
        //            for (int s = i * 100; s < (i + 1) * 100; s++)
        //            {
        //                try
        //                {
        //                    // Получаем юзера
        //                    var resoult = api.UserLogic.UserHasUnitSoldier(new User() { Id = users[s].IdUser });

        //                    if (resoult == false)
        //                    {
        //                        Console.BackgroundColor = ConsoleColor.Red;
        //                        Console.WriteLine($"{s}) {users[s].IdUser} - не имеет в/ч на странице");
        //                    }
        //                    else
        //                    {
        //                        Console.BackgroundColor = ConsoleColor.Green;
        //                        Console.WriteLine($"{s}) {users[s].IdUser} - имеет в/ч на странице");

        //                        users[s].HaveUnitSoldier = true;
        //                        db.SaveChangesAsync();
        //                    }



        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.BackgroundColor = ConsoleColor.Yellow;
        //                    Console.WriteLine($"{ex.Message}");
        //                }


        //            }
        //        });
        //    }
        //}

        // поиск по вузу
        static void FindOnUniversityVK()
        {
            // 13 - Литва
            // 1932227 - Вильнюс
            // 303420 - Вильнюсское высшее командное училище радиоэлектроники ПВО(ВВКУРЭ ПВО)


            // 2 - Украина
            // 314 - Киев
            // 196483 - НА СБУ

            var api = new VkApi();

            api.Authorize(new ApiAuthParams
            {
                ApplicationId = 123456,
                Login = "89114876557",
                Password = "Simplepass19",
                Settings = Settings.All
            });


            ArmyVkAPI.MyApiVK vkapi = new ArmyVkAPI.MyApiVK();
            vkapi.Authorization("89114876557", "Simplepass19");

            try
            {
                var user = api.Users.Get(new long[]
                {
                            16862696
                }, VkNet.Enums.Filters.ProfileFields.All).FirstOrDefault();



                var friends = api.Friends.Get(new FriendsGetParams()
                {
                    UserId = 16862696,
                    Fields = ProfileFields.All
                });

                int id = 0;
                foreach (var friend in friends)
                {

                    Console.WriteLine($"{++id} Проверяем {friend.FirstName} {friend.LastName} - ");

                    foreach (var university in friend.Universities)
                    {
                        if (university.Id == 196483)
                        {
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.WriteLine($"У юзера {user.Id} есть друг (vk.com/id{friend.Id}) {friend.FirstName} {friend.LastName} имеет на странице НА СБУ");
                            Console.BackgroundColor = ConsoleColor.Red;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.WriteLine($"У юзера {user.Id} есть друг (vk.com/id{friend.Id}) {friend.FirstName} {friend.LastName} вуз {university.Name}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine(ex.Message);
            }

        }


        static void GetWordForMandrykin()
        {
            using (var db = new LastDB())
            {

                //var us = db.usAllData.Where(i => i.MestoRab == "в/ч 95043" && i.BorderPropyska == "Авиаперелет" && i.CountryVisit != "КАЗАХСТАН" && i.CountryVisit != "УЗБЕКИСТАН" && i.CountryVisit != "Сирия" && i.CountryVisit != "АРМЕНИЯ" && i.CountryVisit != "АЗЕРБАЙДЖАН").GroupBy(s => s.fio).ToList(); // курортные страны все время
                //var us = db.usAllData.Where(i => i.MestoRab == "в/ч 95043" && i.DateVisit.Value.Year > 2014 && i.BorderPropyska == "Авиаперелет" && i.CountryVisit != "КАЗАХСТАН" && i.CountryVisit != "УЗБЕКИСТАН" && i.CountryVisit != "Сирия" && i.CountryVisit != "АРМЕНИЯ" && i.CountryVisit != "АЗЕРБАЙДЖАН").GroupBy(s => s.fio).ToList(); // курортные страны последние 5 лет




                //var us = db.usAllData.Where(i => i.MestoRab == "в/ч 95043" && i.DateVisit.Value.Year > 2014 && (i.CountryVisit == "КАЗАХСТАН" || i.CountryVisit == "УЗБЕКИСТАН" || i.CountryVisit == "АРМЕНИЯ" || i.CountryVisit == "АЗЕРБАЙДЖАН" || i.CountryVisit == "УКРАИНА" || i.CountryVisit == "МОЛДОВА" || i.CountryVisit == "КЫРГЫЗСТАН" || i.CountryVisit == "ТАДЖИКИСТАН" || i.CountryVisit == "АБХАЗИЯ")).GroupBy(s => s.fio).ToList(); // Россия-СНГ последние 5 лет
                //var us = db.usAllData.Where(i => i.MestoRab == "в/ч 95043" && (i.CountryVisit == "КАЗАХСТАН" || i.CountryVisit == "УЗБЕКИСТАН" || i.CountryVisit == "АРМЕНИЯ" || i.CountryVisit == "АЗЕРБАЙДЖАН" || i.CountryVisit == "УКРАИНА" || i.CountryVisit == "МОЛДОВА" || i.CountryVisit == "КЫРГЫЗСТАН" || i.CountryVisit == "ТАДЖИКИСТАН" || i.CountryVisit == "АБХАЗИЯ")).GroupBy(s => s.fio).ToList(); // Россия-СНГ все время

                //var us = db.usAllData.Where(i => i.MestoRab == "в/ч 95043").GroupBy(s => s.fio).ToList(); // выезды все время
                //var us = db.usAllData.Where(i => i.MestoRab == "в/ч 95043" && i.DateVisit.Value.Year > 2014).GroupBy(s => s.fio).ToList(); // выезды последние 5 лет

                //var us = db.usAllData.Where(i => i.MestoRab == "в/ч 95043" && i.BorderPropyska == "Россия-Польша").GroupBy(s => s.fio).ToList(); // выезды россия-польша
                //var us = db.usAllData.Where(i => i.MestoRab == "в/ч 95043" && i.DateVisit.Value.Year > 2014 && i.BorderPropyska == "Россия-Польша").GroupBy(s => s.fio).ToList(); // выезды россия-польша все время

                //var us = db.usAllData.Where(i => i.MestoRab == "в/ч 95043" && i.BorderPropyska == "Россия-Литва").GroupBy(s => s.fio).ToList(); // выезды россия-литва
                //var us = db.usAllData.Where(i => i.MestoRab == "в/ч 95043" && i.DateVisit.Value.Year > 2014 && i.BorderPropyska == "Россия-Литва").GroupBy(s => s.fio).ToList(); // выезды россия-литва

                //var us = db.usAllData.Where(i => i.MestoRab == "в/ч 95043" && i.DateVisit.Value.Year > 2014 && i.BorderPropyska.Contains("белоруссия")).GroupBy(s => s.fio).ToList(); // выезды в белоруссию последние 5 лет
                var us = db.usAllData.Where(i => i.MestoRab == "в/ч 95043" && i.BorderPropyska.Contains("белоруссия")).GroupBy(s => s.fio).ToList(); // выезды в белоруссию все время


                foreach (var item in us)
                {
                    //Console.Write(item.Key);

                    //Console.Write(db.usAllData.Where(i => i.fio == item.Key).FirstOrDefault().dataBirth);


                    var DateBirth = db.shtatNEW.Where(i => i.FIO.Contains(item.Key)).FirstOrDefault();

                    //Console.WriteLine(DateBirth?.DateBirth);
                    var data = db.usAllData.Where(i => i.fio == item.Key).FirstOrDefault();


                    string write = $"{item.Key}";

                    if (DateBirth.Дата_рождения.HasValue)
                        write += $", {DateBirth?.Дата_рождения.Value.Day}.{DateBirth?.Дата_рождения.Value.Month}.{DateBirth?.Дата_рождения.Value.Year}";

                    if (!string.IsNullOrWhiteSpace(data.CityBirth) && data.CityBirth != "-")
                        write += $", {data.CityBirth}";

                    if (!string.IsNullOrWhiteSpace(data.CountryBirth) && data.CountryBirth != "-")
                        write += $", {data.CountryBirth}";

                    write += ";";

                    Console.WriteLine(write);
                }
            }


        }

        static void Main()
        {
            var services = new ServiceCollection();
            services.AddAudioBypass();
            var api = new VkApi(services);

            api.Authorize(new ApiAuthParams
            {
                ApplicationId = 123456,
                Login = "89629007965",
                Password = "andrey06122SASISA",
                Settings = Settings.All
            });


            var get = api.Groups.Get(new GroupsGetParams()
            {
                UserId = 91071697,
                Count = 1000,
                Extended = true,
                Fields = GroupsFields.All
            });
            

            var audious = api.Audio.Get(new AudioGetParams()
            {
                OwnerId = 290020742,
                Count = 6000                
            });

            //var videous = api.Video.Get(new VideoGetParams()
            //{
            //    OwnerId = 81298243          
            //});            

            foreach (var item in audious)
            {
                if (item.Title.Contains("зиг"))
                    Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine($"{item.Artist} {item.Date} (айди записи {item.Id}) - {item.Title} айди создателя {item.OwnerId}");
                Console.BackgroundColor = ConsoleColor.Black;

                //item.Duration
            }


            //foreach (var item in get)
            //{
            //    var a = item.IsClosed.Value;
                
            //    Console.WriteLine($"{item.Id}) {item.Name} - {item.MembersCount} {item.IsClosed.Value} https://vk.com/club{item.Id}");
            //}

            Console.WriteLine("end");
            Console.ReadKey();
        }


        #region Word

        // Вставить картинку 800х400
        static void InsertPictureOnProvider()
        {
            // Создаём объект word
            Microsoft.Office.Interop.Word._Application OneWord = new Microsoft.Office.Interop.Word.Application();

            // Создаем документ
            var OneDoc = OneWord.Documents.Add();

            // Параметры (проверить позже)
            object f = false;
            object t = true;
            object range = Type.Missing;            
            
            for (int i = 1; i <= 10; i++)
            {
                OneWord.ActiveDocument.Characters.Last.Select();
                OneWord.Selection.Collapse();
                OneDoc.InlineShapes.AddPicture(@"D:\user.png", ref f, ref t, ref range);
                OneWord.ActiveDocument.Characters.Last.Select();
                OneWord.Selection.Collapse();
                OneDoc.Content.InsertAfter($"Рисунок {i}\n");

                if (i % 2 == 0)
                {
                    OneWord.ActiveDocument.Characters.Last.Select();
                    OneWord.Selection.Collapse();

                    OneWord.Selection.InsertBreak(Microsoft.Office.Interop.Word.WdBreakType.wdPageBreak);
                }
            }

            // Выходим и закрываем
            OneDoc.SaveAs2(@"D:\SaveDocFile.docx");
            OneDoc.Close();
            OneWord.Quit();
        }

        static void InsertTextOnShtat()
        {
            using (shtatDB db = new shtatDB())
            {
                var items = db.shtat49289_.ToList();

                // Создаём объект word
                Microsoft.Office.Interop.Word._Application OneWord = new Microsoft.Office.Interop.Word.Application();


                OneWord.WindowState = Word.WdWindowState.wdWindowStateNormal;

                // Создаем документ
                var OneDoc = OneWord.Documents.Add();


                foreach (var item in items)
                {

                    OneDoc.Content.InsertAfter($"{item.family} {item.name} {item.surname}, {item.birth}\n");


                }

                // Выходим и закрываем
                OneDoc.SaveAs2(@"D:\shtat49289.docx");
                OneDoc.Close();
                OneWord.Quit();
            }
        }


        static void InsertText()
        {
            // Создаём объект word
            Microsoft.Office.Interop.Word._Application OneWord = new Microsoft.Office.Interop.Word.Application();


            OneWord.WindowState = Word.WdWindowState.wdWindowStateNormal;

            // Создаем документ
            var OneDoc = OneWord.Documents.Add();

            for (int i = 1; i <= 100; i++)
            {
                OneDoc.Content.InsertAfter($"tested {i}\n");
                if (i % 2 == 0)
                {
                    OneWord.ActiveDocument.Characters.Last.Select();
                    OneWord.Selection.Collapse();

                    OneWord.Selection.InsertBreak(Microsoft.Office.Interop.Word.WdBreakType.wdPageBreak);
                }

            }

            // Выходим и закрываем
            OneDoc.SaveAs2(@"D:\SaveDocFile.docx");
            OneDoc.Close();
            OneWord.Quit();
        }
        static void InsertToWaterMark()
        {
            // Создаём объект документа
            Word.Document doc = null;
            try
            {
                // Создаём объект приложения
                Word.Application app = new Word.Application();
                // Путь до шаблона документа
                string source = @"D:\\test.docx";
                // Открываем
                doc = app.Documents.Open(source);
                doc.Activate();


                // test
                List<string> tested = new List<string>();
                tested.Reverse();

                // Добавляем информацию
                // wBookmarks содержит все закладки
                Word.Bookmarks wBookmarks = doc.Bookmarks;
                Word.Range wRange;
                int i = 0;
                string[] data = new string[4] { "27", "Alex", "Gulynin", "tested" };
                Array.Reverse(data);

                foreach (Word.Bookmark mark in wBookmarks)
                {

                    wRange = mark.Range;
                    wRange.Text = data[i];
                    i++;
                }

                // Закрываем документ
                doc.Close();
                doc = null;
            }
            catch (Exception ex)
            {
                // Если произошла ошибка, то
                // закрываем документ и выводим информацию
                doc.Close();
                doc = null;
                Console.WriteLine("Во время выполнения произошла ошибка!");
                Console.ReadLine();
            }
        }

        #endregion

        static void testingMethodApi()
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

            var api = new VkApi();

            api.Authorize(new ApiAuthParams
            {
                ApplicationId = 123456,
                Login = "89114876557",
                Password = "Simplepass19",
                Settings = Settings.All
            });


            //var cities = api.Database.GetCities(new VkNet.Model.RequestParams.Database.GetCitiesParams() { CountryId = 1, Count = 100 });

            //foreach (var item in cities)
            //{
            //    Console.WriteLine($"{item.Title} - id {item.Id}");
            //}
            // 61 - Калининград

            var searchUser = api.Users.Search(new UserSearchParams()
            {
                Query = "Алексей Авдеев",
                Count = 1000,
                //City = 61,
                BirthDay = 20,
                BirthMonth = 3
            });

            foreach (var item in searchUser)
            {
                Console.WriteLine($"{item.FirstName} {item.LastName} - {item.Id}");
            }


            //var search = api.NewsFeed.Search(new NewsFeedSearchParams
            //{
            //    Count = 200,
            //    Extended = true,
            //    Fields = UsersFields.All,
            //    StartTime = new DateTime(2018, 1, 1)
            //});

            //int i = 0;
            //foreach (var item in search.Items)
            //{
            //    if (item.OwnerId > 0 && item.FromId > 0)
            //    {

            //        Console.BackgroundColor = ConsoleColor.Green;
            //        Console.WriteLine($"{++i}) {item.Date}");
            //        Console.BackgroundColor = ConsoleColor.Black;
            //        Console.WriteLine($"{item.Text}");
            //    }
            //}

            //var users = api.UserLogic.GetFriendsUser(235922528, true);


            //var filter = users.Where(s => s.BirthDate != null).Where(i => i.BirthDate.Contains("3.8")).ToList();

            Console.WriteLine("end");
        }
    }
}
