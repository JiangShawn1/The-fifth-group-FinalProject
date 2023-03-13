using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

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

        [Column("QuestionType")]
        [Required]
        [StringLength(256)]
        [DisplayName("問題類型")]
        public string QuestionType1 { get; set; } = null!;

        public virtual ICollection<CommonQuestion> CommonQuestions { get; set; }
        public virtual ICollection<CustomerFeedback> CustomerFeedbacks { get; set; }
    }
}
