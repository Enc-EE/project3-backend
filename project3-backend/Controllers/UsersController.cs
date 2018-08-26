using project3_backend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace project3_backend.Controllers
{
    public class UsersController : BaseController
    {
        [HttpGet]
        [Route("api/users/me")]
        public User Get()
        {
            Login();
            return AuthenticatedUser;
        }

        [HttpGet]
        [Route("api/users")]
        public List<User> GetAll()
        {
            Login();

            List<User> users = new List<User>();
            using (var ctx = new Project3Context(AuthenticatedUser))
            {
                users = ctx.Users.ToList();
            }
            return users.Select(x => new User()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }
    }
}