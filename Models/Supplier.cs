using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalProject.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            Contests = new HashSet<Contest>();
        }

        public int SupplierId { get; set; }
        public string SupplierName { get; set; } = null!;
        public string SupplierTel { get; set; } = null!;
        public string SupplierAdd { get; set; } = null!;

        public virtual ICollection<Contest> Contests { get; set; }
    }
}
