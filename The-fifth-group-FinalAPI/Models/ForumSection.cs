﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalAPI.Models
{
    public partial class ForumSection
    {
        public ForumSection()
        {
            ForumSectionBranches = new HashSet<ForumSectionBranches>();
        }

        public int Id { get; set; }
        public string SectionName { get; set; }

        public virtual ICollection<ForumSectionBranches> ForumSectionBranches { get; set; }
    }
}