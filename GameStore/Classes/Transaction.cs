using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Classes
{
    internal class Transaction
    {
        public DateTime dateTime { get; private set; }
        public float Cost { get; private set; }

        public Transaction(DateTime dateTime, float cost)
        {
            this.dateTime = dateTime;
            this.Cost = cost;
        }
        public void ShowInfo()
        {
            Console.WriteLine("Date and time of purchase: " + dateTime.ToString());
            Console.WriteLine("Cost: " + Cost.ToString());
        }
    }
}
