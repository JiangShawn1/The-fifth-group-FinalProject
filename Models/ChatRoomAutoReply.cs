using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalProject.Models
{
    public partial class ChatRoomAutoReply
    {
        public int Id { get; set; }
        public DateTime SentTime { get; set; }
        public int AutoReplyId { get; set; }
        public int ChatRoomId { get; set; }

        public virtual AutoReply AutoReply { get; set; } = null!;
        public virtual ChatRoom ChatRoom { get; set; } = null!;
    }
}
