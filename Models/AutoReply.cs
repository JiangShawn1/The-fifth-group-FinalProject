using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalProject.Models
{
    public partial class AutoReply
    {
        public AutoReply()
        {
            AutoReplyKeyWords = new HashSet<AutoReplyKeyWord>();
            ChatRoomAutoReplies = new HashSet<ChatRoomAutoReply>();
        }

        public int Id { get; set; }
        public string AutoReplyContent { get; set; } = null!;

        public virtual ICollection<AutoReplyKeyWord> AutoReplyKeyWords { get; set; }
        public virtual ICollection<ChatRoomAutoReply> ChatRoomAutoReplies { get; set; }
    }
}
