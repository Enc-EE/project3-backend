using project3_backend.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace project3_backend.Models
{
    public class List : IDbEntity
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public User Owner { get; set; }
        public ICollection<ListItem> ListItems { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
    }
}