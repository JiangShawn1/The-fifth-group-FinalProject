using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalProject.Models
{
    public partial class CommonAnswer
    {
        public int Id { get; set; }
        public int CommonQuestionId { get; set; }
        public string Answer { get; set; } = null!;

        public virtual CommonQuestion CommonQuestion { get; set; } = null!;
    }
}
