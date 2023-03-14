using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalProject.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string OrderNumber { get; set; } = null!;
        public DateTime? CreateAt { get; set; }
        public int OrderType { get; set; }
        public int OrderStatus { get; set; }
        public int TradeStatus { get; set; }
        public int? UseCoupon { get; set; }
        public int Amount { get; set; }
        public string? ShippingMethod { get; set; }
        public string? OrderAddress { get; set; }
        public string OrderContent { get; set; } = null!;

        public virtual Member Member { get; set; } = null!;
        public virtual Coupon? UseCouponNavigation { get; set; }
    }
}
