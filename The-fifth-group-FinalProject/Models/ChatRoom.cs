using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalProject.Models
{
    public partial class ChatRoom
    {
        public ChatRoom()
        {
            ChatContents = new HashSet<ChatContent>();
            ChatRoomAutoReplies = new HashSet<ChatRoomAutoReply>();
        }

        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public virtual ICollection<ChatContent> ChatContents { get; set; }
        public virtual ICollection<ChatRoomAutoReply> ChatRoomAutoReplies { get; set; }
    }
}
