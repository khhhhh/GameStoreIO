using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Classes
{
    internal class Game : ICloneable
    {
        public string Name { get; private set; }
        public string Genre { get; private set; }
        public float Cost { get; private set; }
        public int PlayedTime { get; private set; }

        public Game(string name,
                    string genre,
                    float cost)
        {
            this.Name = name;
            this.Genre = genre;
            this.Cost = cost;
            this.PlayedTime = 0;
        }

        public void Play(int time = 1)
        {
            PlayedTime += time;
        }

        public override string ToString()
        {
            string costStr = (Cost == 0) ? "Free" : Cost + "$";
            string sInEnd = (PlayedTime == 1) ? "." : "s.";
            string retStr = String.Format("{0}, {1}, {2}", Name, Genre, costStr);
            if (PlayedTime > 0)
                retStr += $", played time: {PlayedTime} hour{sInEnd}";

            return retStr;
        }

        public object Clone()
        {
            return new Game(Name, Genre, Cost);
        }
    }
}
