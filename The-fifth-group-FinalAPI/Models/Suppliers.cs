﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalAPI.Models
{
    public partial class Suppliers
    {
        public Suppliers()
        {
            Contests = new HashSet<Contests>();
        }

        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierTel { get; set; }
        public string SupplierAdd { get; set; }

        public virtual ICollection<Contests> Contests { get; set; }
    }
}