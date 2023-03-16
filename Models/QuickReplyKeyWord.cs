using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalProject.Models
{
    public partial class QuickReplyKeyWord
    {
        public int Id { get; set; }
        public int QuickReplyId { get; set; }
        public string KeyWord { get; set; } = null!;

        public virtual QuickReply QuickReply { get; set; } = null!;
    }
}
