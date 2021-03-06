﻿using Newtonsoft.Json;
using project3_backend.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace project3_backend.Models
{
    public class ListItem : IDbEntity
    {
        [Key]
        public long Id { get; set; }

        [JsonIgnore]
        public List List { get; set; }

        public string Name { get; set; }
        public bool IsSelected { get; set; }
        public ListItemGroup ListItemGroup { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
    }
}