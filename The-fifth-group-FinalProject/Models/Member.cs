using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace The_fifth_group_FinalProject.Models
{
    public partial class Member
    {
        public Member()
        {
            CartItems = new HashSet<CartItem>();
            ForumSectionBranch1TopicsThreads = new HashSet<ForumSectionBranch1TopicsThread>();
            ForumSectionBranches = new HashSet<ForumSectionBranch>();
            MembersCoupons = new HashSet<MembersCoupon>();
            Orders = new HashSet<Order>();
        }

        public int MemberId { get; set; }
        [Display(Name = "名字")]
        [Required]
        public string Name { get; set; } = null!;

        [Display(Name = "帳號")]
        [Required]
        public string Account { get; set; } = null!;
        [Display(Name = "密碼")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [Display(Name = "電話")]
        [Required]
        public string Phone { get; set; } = null!;
        [Display(Name = "信箱")]
        [Required]
        public string Mail { get; set; } = null!;
        public bool Subscription { get; set; }
        public bool IsConfirmed { get; set; }
        public bool Freeze { get; set; }
        public int State { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<ForumSectionBranch1TopicsThread> ForumSectionBranch1TopicsThreads { get; set; }
        public virtual ICollection<ForumSectionBranch> ForumSectionBranches { get; set; }
        public virtual ICollection<MembersCoupon> MembersCoupons { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
