using GameStore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Classes
{
    internal class Admin
    {
        IAdmin store;

        public bool AddGame(int index = -1)
        {
            string? name, genre;
            float cost;
            bool isNew = (index == -1);
            Console.Write("Enter {0} name: ", isNew ? "" : "new");
            name = Console.ReadLine();

            Console.Write("Enter {0} genre: ", isNew ? "" : "new");
            genre = Console.ReadLine();

            Console.Write("Enter {0} cost: ", isNew ? "" : "new");
            if (!float.TryParse(Console.ReadLine(), out cost))
            {
                Console.WriteLine("Wrong cost!");
                return false;
            }
            if (name == null || genre == null)
            {
                Console.WriteLine("Wrong name/genre!");
                return false;
            }

            Game game = new Game(name, genre, cost);
            if (index == -1)
                store.AddGame(game);
            else
                store.ModifyGame(index, game);
            return true;
        }
        public bool RemoveGame()
        {
            Console.WriteLine("All games:");
            try
            {
                store.ShowStoreGames();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            Console.WriteLine();

            Console.WriteLine("Write down ID of a game:");
            int index;
            if (!int.TryParse(Console.ReadLine(), out index))
                return false;
            if (index > 0)
            {
                store.RemoveGame(index);
                return true;
            }
            else
                return false;
        }
        public bool ModifyGame()
        {
            Console.WriteLine("All games:");
            try
            {
                store.ShowStoreGames();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Thread.Sleep(1000);
                return false;
            }

            Console.WriteLine();

            Console.WriteLine("Write down ID of a game:");
            int index;
            if (!int.TryParse(Console.ReadLine(), out index))
                return false;

            if (index > 0)
                return AddGame(index);
            else
                return false;
        }

        internal void ShowCLI()
        {
            Console.WriteLine("You have logged in Admin account!");
            Thread.Sleep(1000);
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Choose action:");
                Console.WriteLine("1. Add game");
                Console.WriteLine("2. Modify game");
                Console.WriteLine("3. Remove game");
                Console.WriteLine("[any]. Logout");

                var option = Console.ReadKey();
                Console.WriteLine();
                switch (option.Key)
                {
                    case ConsoleKey.D1:
                        if (!AddGame())
                        {
                            Thread.Sleep(1000);
                            Console.WriteLine("Game wasn't added due to Error!");
                        }
                        break;


                    case ConsoleKey.D2:
                        if (!ModifyGame())
                        {
                            Thread.Sleep(1000);
                            Console.WriteLine("Game wasn't modified due to Error!");
                        }
                        break;

                    case ConsoleKey.D3:
                        if (!RemoveGame())
                        {
                            Thread.Sleep(1000);
                            Console.WriteLine("Game wasn't removed due to Error!");
                        }
                        break;

                    default:
                        return;
                }
            }
        }
        public Admin(Store store)
        {
            this.store = store;
        }
    }
}
