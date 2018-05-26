using petsitterbackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace petsitterbackend.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        [HttpGet]
        [Route("isalive")]
        public string IsAlive()
        {
            return "Server is alive";
        }

        [Authorize]
        [HttpGet]
        [Route("getallusers")]
        public List<User> GetAllUsers()
        {
            List<User> oList = new List<User>();
            oList.Add(new User(1, "Mario", "Rossi", 1111111111L, "+396565655454", "mario@rossi.it", "mariorossi"));
            oList.Add(new User(2, "Giusy", "Verdi", 121212111L, "+396455454545", "giuseppe@verdi.it", "giuseppeverdi"));


            return oList;
        }
    }
}
