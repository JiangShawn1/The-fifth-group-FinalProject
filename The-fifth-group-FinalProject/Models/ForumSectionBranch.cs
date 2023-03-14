using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace The_fifth_group_FinalProject.Models
{
    public partial class ForumSectionBranch
    {
        public ForumSectionBranch()
        {
            ForumSectionBranch1Topics = new HashSet<ForumSectionBranch1Topic>();
        }

        public int Id { get; set; }

        public int SectionNameId { get; set; }
        public string BranchName { get; set; } = null!;
        public string SectionAdministrator { get; set; } = null!;
        public int AdministratorId { get; set; }
        public virtual Member Administrator { get; set; } = null!;
        public virtual ForumSection SectionName { get; set; } = null!;
        public virtual ICollection<ForumSectionBranch1Topic> ForumSectionBranch1Topics { get; set; }
    }
}
