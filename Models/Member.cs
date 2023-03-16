using System;
using System.Collections.Generic;

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
        public string Name { get; set; } = null!;
        public string Account { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Phone { get; set; } = null!;
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
