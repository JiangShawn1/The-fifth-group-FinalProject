using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalProject.Models
{
    public partial class ForumSectionBranch1TopicsThread
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public int TopicState { get; set; }
        public int ReplyNumber { get; set; }
        public string ReplyContent { get; set; } = null!;
        public DateTime ReplyTime { get; set; }
        public int ReplyState { get; set; }
        public int ReplyMemberId { get; set; }

        public virtual Member ReplyMember { get; set; } = null!;
        public virtual ForumSectionBranch1Topic Topic { get; set; } = null!;
    }
}
