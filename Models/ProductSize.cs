using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalProject.Models
{
    public partial class ProductSize
    {
        public ProductSize()
        {
            Stocks = new HashSet<Stock>();
        }

        public int Id { get; set; }
        public string Size { get; set; } = null!;

        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
