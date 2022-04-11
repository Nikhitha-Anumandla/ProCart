﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCart.core.Models
{
    public class Products
    {
        public string Id { get; set; }
        [StringLength(20)]
        [Display(Name="Product Name")]
        public string Name { get; set; }
        [Range(0,1000)]
        public decimal Price { get; set; }
        public string  Description { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }
        public Products()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}