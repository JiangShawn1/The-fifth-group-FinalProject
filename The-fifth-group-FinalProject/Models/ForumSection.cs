using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace The_fifth_group_FinalProject.Models
{
    public partial class ForumSection
    {



        public ForumSection()
        {
            ForumSectionBranches = new HashSet<ForumSectionBranch>();
        }

        public int Id { get; set; }

        [Column("SectionName")]
        [DisplayName("問題類型")]
        public string SectionName { get; set; } = null!;

        public virtual ICollection<ForumSectionBranch> ForumSectionBranches { get; set; }
    }
}
