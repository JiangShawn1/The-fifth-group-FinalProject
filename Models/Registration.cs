using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalProject.Models
{
    public partial class Registration
    {
        public Registration()
        {
            RegistrationInformations = new HashSet<RegistrationInformation>();
        }

        public int Id { get; set; }
        public int MemberId { get; set; }
        public int ContestCategoryId { get; set; }
        public bool PaymentStatus { get; set; }

        public virtual ContestCategory ContestCategory { get; set; } = null!;
        public virtual ICollection<RegistrationInformation> RegistrationInformations { get; set; }
    }
}
