﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalAPI.Models
{
    public partial class Products
    {
        public Products()
        {
            CartItems = new HashSet<CartItems>();
            Stocks = new HashSet<Stocks>();
        }

        public int Id { get; set; }
        public int BrandId { get; set; }
        public string ProductName { get; set; }
        public string ProductIntroduce { get; set; }
        public int ColorId { get; set; }
        public int Price { get; set; }
        public string ImageUrl { get; set; }

        public virtual Brands Brand { get; set; }
        public virtual Colors Color { get; set; }
        public virtual ICollection<CartItems> CartItems { get; set; }
        public virtual ICollection<Stocks> Stocks { get; set; }
    }
}