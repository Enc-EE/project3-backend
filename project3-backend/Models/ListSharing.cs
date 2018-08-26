using project3_backend.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace project3_backend.Models
{
    public class ListSharing : IDbEntity
    {
        [Key]
        public long Id { get; set; }

        public User User { get; set; }
        public List List { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
    }
}