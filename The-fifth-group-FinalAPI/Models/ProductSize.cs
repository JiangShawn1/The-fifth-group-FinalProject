﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalAPI.Models
{
    public partial class ProductSize
    {
        public ProductSize()
        {
            Stocks = new HashSet<Stocks>();
        }

        public int Id { get; set; }
        public string Size { get; set; }

        public virtual ICollection<Stocks> Stocks { get; set; }
    }
}