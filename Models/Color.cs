using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalProject.Models
{
    public partial class Color
    {
        public Color()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Color1 { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
