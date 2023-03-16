using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalProject.Models
{
    public partial class Stock
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ProductSizeId { get; set; }
        public int Stock1 { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual ProductSize ProductSize { get; set; } = null!;
    }
}
