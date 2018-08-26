using Newtonsoft.Json;
using project3_backend.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace project3_backend.Models
{
    public class ListItemGroup : IDbEntity
    {
        public ListItemGroup()
        {
            ListItems = new List<ListItem>();
        }

        [Key]
        public long Id { get; set; }

        [JsonIgnore]
        public List List { get; set; }

        public string Name { get; set; }
        public List<ListItem> ListItems { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
    }
}