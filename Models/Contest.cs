using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalProject.Models
{
    public partial class Contest
    {
        public Contest()
        {
            ContestCategories = new HashSet<ContestCategory>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int SupplierId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime ContestDate { get; set; }
        public DateTime RegistrationDeadline { get; set; }
        public string Area { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string MapUrl { get; set; } = null!;
        public string RegistrationUrl { get; set; } = null!;
        public string Detail { get; set; } = null!;
        public bool Review { get; set; }

        public virtual Supplier Supplier { get; set; } = null!;
        public virtual ICollection<ContestCategory> ContestCategories { get; set; }
    }
}
