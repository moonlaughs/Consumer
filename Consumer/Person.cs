using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consumer
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Pass { get; set; }
        public string UserLogin { get; set; }

        public Person()
        {

        }

        public Person(int id, string firstName, string lastName, int age, string pass, string userLogin)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Pass = pass;
            UserLogin = userLogin;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {FirstName}";
        }
    }
}
