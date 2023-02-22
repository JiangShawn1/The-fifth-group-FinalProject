using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalProject.Models
{
    public partial class CartItem
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }

        public virtual Member Member { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
