using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalProject.Models
{
    public partial class ForumSectionBranch1Topic
    {
        public ForumSectionBranch1Topic()
        {
            ForumSectionBranch1TopicsThreads = new HashSet<ForumSectionBranch1TopicsThread>();
        }

        public int Id { get; set; }
        public int BranchId { get; set; }
        public string Topic { get; set; } = null!;
        public int State { get; set; }

        public virtual ForumSectionBranch Branch { get; set; } = null!;
        public virtual ICollection<ForumSectionBranch1TopicsThread> ForumSectionBranch1TopicsThreads { get; set; }
    }
}
