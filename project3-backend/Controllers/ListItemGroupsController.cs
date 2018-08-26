using project3_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace project3_backend.Controllers
{
    public class ListItemGroupsController : BaseController
    {
        [HttpGet]
        [Route("api/lists/{listId}/list-item-groups")]
        public List<ListItemGroup> GetListItemGroups(long listId)
        {
            Login();
            List<ListItemGroup> listItemGroups = new List<ListItemGroup>();
            using (var ctx = new Project3Context(AuthenticatedUser))
            {
                var list = ctx.Lists.Include("ListItemGroups").Include("ListItems").FirstOrDefault(l => l.Id == listId);
                if (list.ListItemGroups != null)
                {
                    listItemGroups = list.ListItemGroups.ToList();
                }
            }
            listItemGroups.ForEach(x => x.ListItems.ForEach(y => y.ListItemGroup = null));
            return listItemGroups;
        }

        [HttpPost]
        [Route("api/lists/{listId}/list-item-groups")]
        public long CreateListItemGroup(long listId, [FromBody]ListItemGroup listItemGroup)
        {
            Login();
            using (var ctx = new Project3Context(AuthenticatedUser))
            {
                if (listId > 0 && ctx.Lists.Any(l => l.Id == listId))
                {
                    if (listItemGroup.Id > 0 && ctx.ListItemGroups.Any(l => l.Id == listItemGroup.Id))
                    {
                        // update with post not allowed
                    }
                    else
                    {
                        listItemGroup.List = ctx.Lists.Single(l => l.Id == listId);
                        listItemGroup = ctx.ListItemGroups.Add(listItemGroup);
                    }
                    ctx.SaveChanges();
                }
                else
                {
                    // list does not exist
                }
            }
            return listItemGroup.Id;
        }

        [HttpPut]
        [Route("api/lists/{listId}/list-item-groups/{listItemGroupId}")]
        public void UpdateListItemGroup(long listId, long listItemGroupId, [FromBody]ListItemGroup value)
        {
            Login();
            using (var ctx = new Project3Context(AuthenticatedUser))
            {
                var list = ctx.Lists.Include("ListItemGroups").FirstOrDefault(l => l.Id == listId);
                if (list?.ListItemGroups != null)
                {
                    var listItemGroup = list.ListItemGroups.FirstOrDefault(li => li.Id == listItemGroupId);
                    if (listItemGroup != null)
                    {
                        listItemGroup.Name = value.Name;
                        ctx.SaveChanges();
                    }
                }
            }
        }

        [HttpDelete]
        [Route("api/lists/{listId}/list-item-groups/{listItemGroupId}")]
        public void DeleteListItem(long listId, long listItemGroupId)
        {
            Login();
            using (var ctx = new Project3Context(AuthenticatedUser))
            {
                var list = ctx.Lists.Include("ListItemGroups").Include("ListItems").FirstOrDefault(l => l.Id == listId);
                if (list?.ListItemGroups != null)
                {
                    var listItemGroup = list.ListItemGroups.FirstOrDefault(x => x.Id == listItemGroupId);
                    if (listItemGroup != null)
                    {
                        foreach (var listItem in listItemGroup.ListItems.ToList())
                        {
                            ctx.ListItems.Remove(listItem);
                        }
                        ctx.ListItemGroups.Remove(listItemGroup);
                        ctx.SaveChanges();
                    }
                }
            }
        }
    }
}