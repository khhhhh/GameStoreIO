using GameStore.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Interfaces
{
    internal interface IAccount
    {
        public void ShowStoreGames();
        public void ShowAccounts(string username);
        public Game? BuyGame(int index, float availMoney);
        public Account? AddFriend(string username);
    }
}
