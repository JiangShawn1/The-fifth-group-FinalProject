using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalProject.Models
{
    public partial class CustomerFeedback
    {
        public int Id { get; set; }
        public string FeedbackContent { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int QuestionTypeId { get; set; }
        public int Status { get; set; }
        public DateTime CreateTime { get; set; }

        public virtual QuestionType QuestionType { get; set; } = null!;
    }
}
