using project3_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace project3_backend.Controllers
{
    public class UsersController : BaseController
    {
        [Route("api/users/me")]
        public User Get()
        {
            Login();
            return AuthenticatedUser;
        }
    }
}