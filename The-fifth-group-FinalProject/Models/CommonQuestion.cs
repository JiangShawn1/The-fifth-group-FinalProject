using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalProject.Models
{
    public partial class CommonQuestion
    {
        public CommonQuestion()
        {
            CommonAnswers = new HashSet<CommonAnswer>();
        }

        public int Id { get; set; }
        public string Question { get; set; } = null!;
        public int QuestionTypeId { get; set; }

        public virtual QuestionType QuestionType { get; set; } = null!;
        public virtual ICollection<CommonAnswer> CommonAnswers { get; set; }
    }
}
