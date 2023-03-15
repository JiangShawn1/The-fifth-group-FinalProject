namespace The_fifth_group_FinalProject.Models
{
    public class ProductSearchViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
        public IEnumerable<Color> Colors { get; set; }
        public string SelectedBrand { get; set; }
        public string SelectedColor { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }

}
