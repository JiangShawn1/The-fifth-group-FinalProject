﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalAPI.Models
{
    public partial class CommonAnswers
    {
        public int Id { get; set; }
        public int CommonQuestionId { get; set; }
        public string Answer { get; set; }

        public virtual CommonQuestions CommonQuestion { get; set; }
    }
}