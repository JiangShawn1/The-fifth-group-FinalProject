﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalAPI.Models
{
    public partial class Information
    {
        public Information()
        {
            RegistrationInformation = new HashSet<RegistrationInformation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }

        public virtual ICollection<RegistrationInformation> RegistrationInformation { get; set; }
    }
}