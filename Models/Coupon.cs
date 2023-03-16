using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalProject.Models
{
    public partial class Coupon
    {
        public Coupon()
        {
            MembersCoupons = new HashSet<MembersCoupon>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string CouponName { get; set; } = null!;
        public string CouponNumber { get; set; } = null!;
        public int CouponType { get; set; }
        public int CouponDiscount { get; set; }
        public int? CouponQuantity { get; set; }
        public int? AccountQuantity { get; set; }
        public int? MinSpend { get; set; }
        public bool? IsCombine { get; set; }
        public string? CouponImage { get; set; }
        public string? CouponContent { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public int? CorrespondProduct { get; set; }
        public DateTime? CreateAt { get; set; }
        public bool? SoftDelete { get; set; }

        public virtual ICollection<MembersCoupon> MembersCoupons { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
