using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace petsitterbackend.Models
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public long birthDate { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string password { get; set; }


        public User() { }

        public User(int id, string name, string surname, long birthdate, string phoneNumber, string email, string password)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.birthDate = birthdate;
            this.phoneNumber = phoneNumber;
            this.email = email;
            this.password = password;
        }
    }

}