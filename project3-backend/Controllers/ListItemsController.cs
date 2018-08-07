using project3_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace project3_backend.Controllers
{
    public class ListItemsController : BaseController
    {
        [Route("api/lists/{listId}/list-items")]
        public List<ListItem> GetListItems(long listId)
        {
            Login();
            List<ListItem> listItems = new List<ListItem>();
            using (var ctx = new Project3Context(AuthenticatedUser))
            {
                var list = ctx.Lists.Include("ListItems").FirstOrDefault(l => l.Owner.Id == AuthenticatedUser.Id && l.Id == listId);
                if (list.ListItems != null)
                {
                    listItems = list.ListItems.ToList();
                }
            }
            return listItems;
        }

        [Route("api/lists/{listId}/list-items")]
        public long PostListItem(long listId, [FromBody]ListItem listItem)
        {
            Login();
            using (var ctx = new Project3Context(AuthenticatedUser))
            {
                if (listId > 0 && ctx.Lists.Any(l => l.Id == listId))
                {
                    if (listItem.Id > 0 && ctx.ListItems.Any(l => l.Id == listItem.Id))
                    {
                        // update with post not allowed
                    }
                    else
                    {
                        listItem.List = ctx.Lists.Include("ListItems").Single(l => l.Id == listId);
                        listItem = ctx.ListItems.Add(listItem);
                    }
                    ctx.SaveChanges();
                }
                else
                {
                    // list does not exist
                }
            }
            return listItem.Id;
        }

        [Route("api/lists/{listId}/list-items/{listItemId}")]
        public void PutListItem(long listId, long listItemId, [FromBody]ListItem value)
        {
            Login();
            using (var ctx = new Project3Context(AuthenticatedUser))
            {
                var list = ctx.Lists.Include("ListItems").FirstOrDefault(l => l.Id == listId);
                if (list != null && list.ListItems != null)
                {
                    var listItem = list.ListItems.FirstOrDefault(li => li.Id == listItemId);
                    if (listItem != null)
                    {
                        listItem.Name = value.Name;
                        ctx.SaveChanges();
                    }
                }
            }
        }

        [Route("api/lists/{listId}/list-items/{listItemId}")]
        public void DeleteListItem(long listId, long listItemId)
        {
            Login();
            using (var ctx = new Project3Context(AuthenticatedUser))
            {
                var list = ctx.Lists.Include("ListItems").FirstOrDefault(l => l.Id == listId);
                if (list != null && list.ListItems != null)
                {
                    var listItem = list.ListItems.FirstOrDefault(li => li.Id == listItemId);
                    if (listItem != null)
                    {
                        ctx.ListItems.Remove(listItem);
                        ctx.SaveChanges();
                    }
                }
            }
        }
    }
}