﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalAPI.Models
{
    public partial class AutoReplyKeyWords
    {
        public int Id { get; set; }
        public int AutoReplyId { get; set; }
        public string KeyWord { get; set; }

        public virtual AutoReplies AutoReply { get; set; }
    }
}