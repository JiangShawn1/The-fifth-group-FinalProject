using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalProject.Models
{
    public partial class AutoReplyKeyWord
    {
        public int Id { get; set; }
        public int AutoReplyId { get; set; }
        public string KeyWord { get; set; } = null!;

        public virtual AutoReply AutoReply { get; set; } = null!;
    }
}
