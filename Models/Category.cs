using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalProject.Models
{
    public partial class Category
    {
        public Category()
        {
            ContestCategories = new HashSet<ContestCategory>();
        }

        public int Id { get; set; }
        public string Category1 { get; set; } = null!;
        public int Distance { get; set; }

        public virtual ICollection<ContestCategory> ContestCategories { get; set; }
    }
}
