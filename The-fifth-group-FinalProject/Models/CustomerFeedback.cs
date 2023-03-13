﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace The_fifth_group_FinalProject.Models
{
    public partial class CustomerFeedback
    {
        public int Id { get; set; }
        [Required]
        [StringLength(1000)]
        [DisplayName("問題內容")]
        public string FeedbackContent { get; set; } = null!;
        [Required]
        [StringLength(100)]
        [DisplayName("客戶名稱")]
        public string CustomerName { get; set; } = null!;
        [Required]
        [StringLength(256)]
        [DisplayName("信箱")]
        public string Email { get; set; } = null!;
        [DisplayName("問題種類")]
        public int QuestionTypeId { get; set; }
        [DisplayName("狀態")]
        public int Status { get; set; }
        [DisplayName("提問時間")]
        public DateTime CreateTime { get; set; }
        [DisplayName("問題種類")]
        public virtual QuestionType? QuestionType { get; set; } 
    }
}
