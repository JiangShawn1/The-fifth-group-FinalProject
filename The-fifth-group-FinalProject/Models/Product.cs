using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalProject.Models
{
    public partial class Product
    {
        public Product()
        {
            CartItems = new HashSet<CartItem>();
            Stocks = new HashSet<Stock>();
        }

        public int Id { get; set; }
        public int BrandId { get; set; }
        public string ProductName { get; set; } = null!;
        public string ProductIntroduce { get; set; } = null!;
        public int ColorId { get; set; }
        public int Price { get; set; }
        public string ImageUrl { get; set; } = null!;

        public virtual Brand Brand { get; set; } = null!;
        public virtual Color Color { get; set; } = null!;
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
