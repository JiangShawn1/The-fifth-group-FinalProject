﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalAPI.Models
{
    public partial class ChatContents
    {
        public int Id { get; set; }
        public DateTime SentTime { get; set; }
        public string ChatContent { get; set; }
        public int? MemberId { get; set; }
        public int ChatRoomId { get; set; }
        public int EmployeeId { get; set; }

        public virtual ChatRooms ChatRoom { get; set; }
        public virtual Employees Employee { get; set; }
    }
}