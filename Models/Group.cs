﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIM.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Organization Organization { get; set; }
        public ICollection<Grant> Grants { get; set; }
        public ICollection<User> Users { get; set; }

    }
}