﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalAPI.Models
{
    public partial class OnSale
    {
        public OnSale()
        {
            Brands = new HashSet<Brands>();
        }

        public int Id { get; set; }
        public double Discount { get; set; }

        public virtual ICollection<Brands> Brands { get; set; }
    }
}