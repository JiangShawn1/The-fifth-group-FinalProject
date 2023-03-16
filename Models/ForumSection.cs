using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalProject.Models
{
    public partial class ForumSection
    {
        public ForumSection()
        {
            ForumSectionBranches = new HashSet<ForumSectionBranch>();
        }

        public int Id { get; set; }
        public string SectionName { get; set; } = null!;

        public virtual ICollection<ForumSectionBranch> ForumSectionBranches { get; set; }
    }
}
