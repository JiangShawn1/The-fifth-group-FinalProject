﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalAPI.Models
{
    public partial class CommonQuestions
    {
        public CommonQuestions()
        {
            CommonAnswers = new HashSet<CommonAnswers>();
        }

        public int Id { get; set; }
        public string Question { get; set; }
        public int QuestionTypeId { get; set; }

        public virtual QuestionTypes QuestionType { get; set; }
        public virtual ICollection<CommonAnswers> CommonAnswers { get; set; }
    }
}