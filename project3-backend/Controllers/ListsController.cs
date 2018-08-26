using project3_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace project3_backend.Controllers
{
    public class ListsController : BaseController
    {
        [Route("api/lists")]
        public IEnumerable<List> GetLists()
        {
            Login();
            List<List> lists;
            using (var ctx = new Project3Context(AuthenticatedUser))
            {
                lists = ctx.Lists.Where(l => l.Owner.Id == AuthenticatedUser.Id).ToList();
                var sharings = ctx.ListSharings.Include("User").Include("List").ToList().Where(x => x.User == AuthenticatedUser);
                lists.AddRange(sharings.Select(x => x.List));
            }
            lists.ForEach(x => x.ListSharings = null);
            return lists;
        }

        [Route("api/lists/{listId}")]
        public List GetList(long listId)
        {
            Login();
            List list;
            using (var ctx = new Project3Context(AuthenticatedUser))
            {
                list = ctx.Lists.Single(l => l.Id == listId);
            }
            return list;
        }

        [Route("api/lists")]
        public long PostList([FromBody]List list)
        {
            Login();
            using (var ctx = new Project3Context(AuthenticatedUser))
            {
                list.Owner = AuthenticatedUser;
                if (list.Id > 0 && ctx.Lists.Any(l => l.Id == list.Id))
                {
                    // update with post not allowed
                }
                else
                {
                    list = ctx.Lists.Add(list);
                }
                ctx.SaveChanges();
            }
            return list.Id;
        }

        [Route("api/lists/{listId}")]
        public void PutList(long listId, [FromBody]List value)
        {
            Login();
            using (var ctx = new Project3Context(AuthenticatedUser))
            {
                var list = ctx.Lists.Include("ListItems").Include("ListItemGroups").FirstOrDefault(l => l.Id == listId);
                if (list != null)
                {
                    if (list.IsGroupingEnabled != value.IsGroupingEnabled)
                    {
                        list.IsGroupingEnabled = value.IsGroupingEnabled;
                        if (list.IsGroupingEnabled)
                        {
                            var group = new ListItemGroup
                            {
                                Name = "group"
                            };
                            foreach (var listItem in list.ListItems)
                            {
                                listItem.ListItemGroup = group;
                            }
                            list.ListItemGroups.Add(group);
                        }
                        else
                        {
                            foreach (var listItemGroup in list.ListItemGroups.ToList())
                            {
                                ctx.ListItemGroups.Remove(listItemGroup);
                            }
                        }
                    }
                    list.Name = value.Name;
                    ctx.SaveChanges();
                }
            }
        }

        [Route("api/lists/{listId}")]
        public void DeleteList(long listId)
        {
            Login();
            using (var ctx = new Project3Context(AuthenticatedUser))
            {
                var list = ctx.Lists.Include("ListItems").Include("ListItemGroups").FirstOrDefault(l => l.Id == listId);
                if (list != null)
                {
                    foreach (var listItem in list.ListItems.ToList())
                    {
                        ctx.ListItems.Remove(listItem);
                    }
                    foreach (var listItemGroup in list.ListItemGroups.ToList())
                    {
                        ctx.ListItemGroups.Remove(listItemGroup);
                    }
                    ctx.Lists.Remove(list);
                    ctx.SaveChanges();
                }
            }
        }
    }
}