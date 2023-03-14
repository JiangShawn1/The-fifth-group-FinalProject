using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalProject.Models
{
    public partial class QuestionType
    {
        public QuestionType()
        {
            CommonQuestions = new HashSet<CommonQuestion>();
            CustomerFeedbacks = new HashSet<CustomerFeedback>();
        }

        public int Id { get; set; }
        public string QuestionType1 { get; set; } = null!;

        public virtual ICollection<CommonQuestion> CommonQuestions { get; set; }
        public virtual ICollection<CustomerFeedback> CustomerFeedbacks { get; set; }
    }
}
