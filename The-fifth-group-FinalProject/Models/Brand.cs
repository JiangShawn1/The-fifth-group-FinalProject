using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalProject.Models
{
    public partial class Brand
    {
        public Brand()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Brand1 { get; set; } = null!;
        public string BrandImageUrl { get; set; } = null!;
        public string BrandIntroduce { get; set; } = null!;
        public int OnSaleId { get; set; }

        public virtual OnSale OnSale { get; set; } = null!;
        public virtual ICollection<Product> Products { get; set; }
    }
}
