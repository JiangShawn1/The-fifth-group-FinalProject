using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalProject.Models
{
    public partial class ContestCategory
    {
        public ContestCategory()
        {
            Registrations = new HashSet<Registration>();
        }

        public int Id { get; set; }
        public int ContestId { get; set; }
        public int CategoryId { get; set; }
        public int Quota { get; set; }
        public int EnterFee { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual Contest Contest { get; set; } = null!;
        public virtual ICollection<Registration> Registrations { get; set; }
    }
}
