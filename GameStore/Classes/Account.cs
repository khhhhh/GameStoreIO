using GameStore.Interfaces;

namespace GameStore.Classes
{
    internal class Account
    {
        float money;
        IAccount store;
        Person person;

        List<Game> games;
        List<Account> friends;
        List<Transaction> transactions;

        public string Username { get; private set; }
        public string Password { get; private set; }

        public Account(string email, string username, string password, IAccount store, string firstName = "", string lastName = "")
        {
            this.Username = username;
            this.Password = password;
            this.store = store;
            games = new List<Game>();
            friends = new List<Account>();
            transactions = new List<Transaction>();
            money = 0;
            person = new Person(firstName, lastName, email);
        }

        internal void ShowCLI()
        {
            Console.WriteLine("You have logged in account!");
            while (true)
            {
                Console.WriteLine("=============================");
                Console.WriteLine("You have {0} $!", money);
                Console.WriteLine("Choose action:");
                Console.WriteLine("1. Buy game");
                Console.WriteLine("2. Show my games");
                Console.WriteLine("3. Show games in store");
                Console.WriteLine("4. Add money");
                Console.WriteLine("5. Play game");
                Console.WriteLine("6. List of friends");
                Console.WriteLine("7. Add Friend");
                Console.WriteLine("8. Remove Friend");
                Console.WriteLine("9. Return game");
                Console.WriteLine("0. Common games with friends");
                Console.WriteLine("[any]. Logout");

                var option = Console.ReadKey();
                Console.WriteLine();

                Console.Clear();
                switch (option.Key)
                {
                    case ConsoleKey.D1:
                        if (!BuyGame())
                            Console.WriteLine("Game wasn't added due to Error!");
                        break;

                    case ConsoleKey.D2:
                        ShowPurchasedGames();
                        break;

                    case ConsoleKey.D3:
                        store.ShowStoreGames();
                        break;

                    case ConsoleKey.D4:
                        AddMoney();
                        Thread.Sleep(1000);
                        break;

                    case ConsoleKey.D5:
                        PlayGames();
                        break;

                    case ConsoleKey.D6:
                        showFriendsList();
                        break;

                    case ConsoleKey.D7:
                        AddNewFriend();
                        break;

                    case ConsoleKey.D8:
                        RemoveFriend();
                        break;

                    case ConsoleKey.D9:
                        ReturnGame();
                        break;

                    case ConsoleKey.D0:
                        ShowCommonGames();
                        break;
                    default:
                        return;
                }
            }
        }

        #region friends
        public void showFriendsList() 
        { 
            if(friends.Count == 0)
            {
                Console.WriteLine("You have no friends! :(");
                return;
            }
            for(int i = 0; i < friends.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i+1, friends[i]);
            }

        }
        public void AddNewFriend() 
        {
            store.ShowAccounts(this.Username);
            Console.Write("Write friend's username: ");
            string? username = Console.ReadLine();

            if(string.IsNullOrEmpty(username))
            {
                Console.WriteLine("Wrong username!");
                return;
            }

            Account? friend = store.AddFriend(username);

            if(friend == null)
            {
                Console.WriteLine("No account found with this username!");
                return;
            }

            if(friends.Contains(friend))
            {
                Console.WriteLine("You are already friends!");
                return;
            }

            if(friend.Equals(this))
            {
                Console.WriteLine("You can't add yourself");
                return;
            }
            friend.friends.Add(this);
            friends.Add(friend);
        }
        public void RemoveFriend() {
            showFriendsList();
            Console.WriteLine("Write ID of friend you want to delete:");
            int index;
            if(!int.TryParse(Console.ReadLine(), out index))
            {
                Console.WriteLine("Wrong ID!");
                return;
            }
            friends.RemoveAt(index-1);
        }

        #endregion

        #region games
        private void PlayGames()
        {
            ShowPurchasedGames();
            if (games.Count == 0)
                return;
            Console.Write("Choose game you want to play: ");
            int index;
            if (!int.TryParse(Console.ReadLine(), out index))
            {
                Console.WriteLine("Wrong ID!");
                return;
            }
            else
                try
                {
                    games[index-1].Play();
                }
                catch
                {
                    Console.WriteLine("Wrong ID!");
                    return;
                }
        }
        public bool BuyGame() 
        {
            store.ShowStoreGames();
            Console.WriteLine("Write down ID of a game: "); 
            int index;
            if (!int.TryParse(Console.ReadLine(), out index))
                return false;
            Game? game = store.BuyGame(index, money);
            if(game == null)
                return false;

            if (games.Contains(game))
            {
                Console.WriteLine("You've already purchased this game!");
                return false;
            }

            Console.Clear();
            Transaction tr = new Transaction(DateTime.Now, game.Cost);
            Console.WriteLine(game);
            tr.ShowInfo();
            transactions.Add(tr);

            money -= tr.Cost;

            games.Add(game);

            return true;
        }
        public void ShowPurchasedGames() 
        {
            if(games.Count == 0)
                Console.WriteLine("You have no games!");
            else
                for(int i = 0; i < games.Count; i++)
                    Console.WriteLine("{0}: {1}", i+1, games[i]);
        }

        public void ReturnGame()
        {
            ShowPurchasedGames();
            Console.Write("Write ID of game you want to return: ");
            int index;

            if(!int.TryParse(Console.ReadLine(), out index))
            {
                Console.WriteLine("Wrong ID!");
                return;
            }

            Game game;
            try
            {
                game = games[--index];
            }
            catch
            {
                Console.WriteLine("Wrong ID!");
                return;
            }

            if(game.PlayedTime > 10)
            {
                Console.WriteLine("You can't return game because you played 10+ hours!");
            }
            else
            {
                money += game.Cost;
                games.Remove(game);
            }
        }
        public void ShowCommonGames()
        {
            foreach(Game game in games)
            {
                bool isCommon = false;
                Console.Write("{0} ( ", game);
                foreach(Account friend in friends)
                {
                    if (friend.games.Where(x => x.Name == game.Name).Count() > 0)
                    {
                        Console.Write("{0} ", friend.Username);
                        isCommon = true;
                    }
                }
                if(!isCommon)
                    Console.Write("No friends have this game ");
                Console.Write(")\n");
            }    
        }
        #endregion

        public void AddMoney() 
        {
            Console.Write("How much do you want to add?: ");
            float money;

            if(!float.TryParse(Console.ReadLine(), out money))
                Console.WriteLine("Error!");
            else
            {
                Console.WriteLine("Money added successfully!");
                this.money += money;
            }
        }
        public override string ToString()
        {
            return String.Format("{0}", Username);
        }
    }
}
