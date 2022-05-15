using GameStore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Classes
{
    internal class Store : IAdmin, IAccount
    {
        List<Account> accounts;
        List<Game> games;

        public Store()
        {
            accounts = new List<Account>();
            games = new List<Game>();
        }

        public void AddAccount(Account account) { accounts.Add(account); }

        void IAccount.ShowStoreGames() => _showStoreGames();
        void IAdmin.ShowStoreGames() => _showStoreGames();
        public void _showStoreGames()
        {
            if (games.Count == 0)
                throw new Exception("No games available!");
            for(int i = 0; i < games.Count; i++)
            {
                Console.WriteLine("{0}: {1}", i+1, games[i]);
            }
        }

        public void ShowAccounts(string username)
        {
            for(int i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].Username != username)
                    Console.WriteLine("- {0}", accounts[i]);
            }
        }

        public Game? BuyGame(int index, float availMoney)
        {
            index--;
            if (availMoney < games[index].Cost)
            {
                Console.WriteLine("Not enougth money on account!");
                return null;
            }
            if (index >= games.Count || index < 0)
            {
                Console.WriteLine("This game doesn't exist!");
                return null;
            }

            return (Game)games[index].Clone();
        }

        public Account? AddFriend(string username)
        {
            foreach (Account account in accounts)
                if (account.Username == username)
                    return account;
            return null;
        }

        public void AddGame(Game game) { games.Add(game); }

        public void RemoveGame(int index) { games.RemoveAt(--index); }

        public void ModifyGame(int index, Game game) { games[--index] = game; }

        public Account? GetAccount(string? username, string? password)
        {
            if (string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password))
                return null;

            foreach(Account account in accounts)
            {
                if (account.Username != username)
                    continue;

                if (account.Password == password)
                    return account;
            }
            return null;
        }
    }
}
