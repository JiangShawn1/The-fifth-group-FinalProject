using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalProject.Models
{
    public partial class MembersCoupon
    {
        public int MemberId { get; set; }
        public int CouponId { get; set; }
        public int CouponsQuantity { get; set; }

        public virtual Coupon Coupon { get; set; } = null!;
        public virtual Member Member { get; set; } = null!;
    }
}
