using GameStore.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Interfaces
{
    internal interface IAdmin
    {
        public void AddGame(Game game);
        public void RemoveGame(int index);
        public void ModifyGame(int index, Game game);
        public void ShowStoreGames();
    }
}
