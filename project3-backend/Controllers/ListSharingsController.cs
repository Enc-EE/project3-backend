using project3_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace project3_backend.Controllers
{
    public class ListItemSharingsController : BaseController
    {
        [HttpGet]
        [Route("api/lists/{listId}/list-sharings")]
        public List<ListSharing> GetAll(long listId)
        {
            Login();
            List<ListSharing> listSharings = new List<ListSharing>();
            using (var ctx = new Project3Context(AuthenticatedUser))
            {
                listSharings = ctx.ListSharings.Include("List").Include("User").Where(l => l.List.Id == listId).ToList();
            }
            listSharings.ForEach(x => x.List.ListSharings = null);
            return listSharings;
        }

        [HttpPost]
        [Route("api/lists/{listId}/list-sharings")]
        public long Create(long listId, [FromBody]ListSharing listSharing)
        {
            Login();
            using (var ctx = new Project3Context(AuthenticatedUser))
            {
                if (listId > 0 && ctx.Lists.Any(l => l.Id == listId))
                {
                    var list = ctx.Lists.Include("ListSharings").First(x => x.Id == listId);
                    if (listSharing?.Id <= 0 && listSharing.User?.Id > 0 && ctx.Users.Any(x => x.Id == listSharing.User.Id))
                    {
                        var user = ctx.Users.First(x => x.Id == listSharing.User.Id);
                        if (!list.ListSharings.Any(x => x.User.Id == listSharing.User.Id))
                        {
                            listSharing.List = list;
                            listSharing.User = user;
                            listSharing = ctx.ListSharings.Add(listSharing);
                        }
                    }
                    else
                    {
                        // user does not exist
                    }
                    ctx.SaveChanges();
                }
                else
                {
                    // list does not exist
                }
            }
            return listSharing.Id;
        }

        [HttpDelete]
        [Route("api/lists/{listId}/list-sharings/{listItemId}")]
        public void Delete(long listId, long listSharingId)
        {
            Login();
            using (var ctx = new Project3Context(AuthenticatedUser))
            {
                var list = ctx.Lists.Include("ListSharings").FirstOrDefault(l => l.Id == listId);
                if (list?.ListSharings != null)
                {
                    var listSharing = list.ListSharings.FirstOrDefault(x => x.Id == listSharingId);
                    if (listSharing != null)
                    {
                        ctx.ListSharings.Remove(listSharing);
                        ctx.SaveChanges();
                    }
                }
            }
        }
    }
}