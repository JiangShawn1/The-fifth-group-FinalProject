﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalAPI.Models
{
    public partial class ForumSectionBranches
    {
        public ForumSectionBranches()
        {
            ForumSectionBranch1Topics = new HashSet<ForumSectionBranch1Topics>();
        }

        public int Id { get; set; }
        public int SectionNameId { get; set; }
        public string BranchName { get; set; }
        public string SectionAdministrator { get; set; }
        public int AdministratorId { get; set; }

        public virtual Members Administrator { get; set; }
        public virtual ForumSection SectionName { get; set; }
        public virtual ICollection<ForumSectionBranch1Topics> ForumSectionBranch1Topics { get; set; }
    }
}