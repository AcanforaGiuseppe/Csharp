﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Category
    {
        [Key]
        public int Category_Id { get; set; }
        public string Name { get; set; }
    }
}