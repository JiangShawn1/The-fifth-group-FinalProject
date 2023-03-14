using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalProject.Models
{
    public partial class OnSale
    {
        public OnSale()
        {
            Brands = new HashSet<Brand>();
        }

        public int Id { get; set; }
        public double Discount { get; set; }

        public virtual ICollection<Brand> Brands { get; set; }
    }
}
