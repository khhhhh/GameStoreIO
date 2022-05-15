using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Classes
{
    internal class Person
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }

        public Person(string FirstName, string LastName, string Email)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
        }


    }
}
