﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalAPI.Models
{
    public partial class ChatRooms
    {
        public ChatRooms()
        {
            ChatContents = new HashSet<ChatContents>();
            ChatRoomAutoReplies = new HashSet<ChatRoomAutoReplies>();
        }

        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public virtual ICollection<ChatContents> ChatContents { get; set; }
        public virtual ICollection<ChatRoomAutoReplies> ChatRoomAutoReplies { get; set; }
    }
}