using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalProject.Models
{
    public partial class QuickReply
    {
        public QuickReply()
        {
            QuickReplyKeyWords = new HashSet<QuickReplyKeyWord>();
        }

        public int Id { get; set; }
        public string QuickReplyContent { get; set; } = null!;

        public virtual ICollection<QuickReplyKeyWord> QuickReplyKeyWords { get; set; }
    }
}
