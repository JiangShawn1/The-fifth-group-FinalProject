﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalAPI.Models
{
    public partial class Colors
    {
        public Colors()
        {
            Products = new HashSet<Products>();
        }

        public int Id { get; set; }
        public string Color { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}