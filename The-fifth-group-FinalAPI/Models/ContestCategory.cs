﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalAPI.Models
{
    public partial class ContestCategory
    {
        public ContestCategory()
        {
            Registration = new HashSet<Registration>();
        }

        public int Id { get; set; }
        public int ContestId { get; set; }
        public int CategoryId { get; set; }
        public int Quota { get; set; }
        public int EnterFee { get; set; }

        public virtual Categories Category { get; set; }
        public virtual Contests Contest { get; set; }
        public virtual ICollection<Registration> Registration { get; set; }
    }
}