using petsitterbackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace petsitterbackend.Manager
{
    public class AuthManager
    {
        public User ValidateUser(string email, string password)
        {
            // Here you can write the code to validate
            // User from database and return accordingly
            // To test we use dummy list here
            var userList = GetUserList();
            var user = userList.FirstOrDefault(x => x.email == email && x.password == password);
            return user;
        }

        public List<User> GetUserList()
        {
            // Create the list of user and return    

            List<User> oList = new List<User>();
            oList.Add(new User(1, "Mario", "Rossi", 1111111111L, "+396565655454","mario@rossi.it", "mariorossi"));
            oList.Add(new User(2, "Giuseppe", "Verdi", 121212111L, "+396455454545", "giuseppe@verdi.it", "giuseppeverdi"));


            return oList;
        }
    }
}