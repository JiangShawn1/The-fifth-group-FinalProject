using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalProject.Models
{
    public partial class ChatContent
    {
        public int Id { get; set; }
        public DateTime SentTime { get; set; }
        public string ChatContent1 { get; set; } = null!;
        public int? MemberId { get; set; }
        public int ChatRoomId { get; set; }
        public int EmployeeId { get; set; }

        public virtual ChatRoom ChatRoom { get; set; } = null!;
        public virtual Employee Employee { get; set; } = null!;
    }
}
