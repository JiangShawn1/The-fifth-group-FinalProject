using The_fifth_group_FinalProject.Models;

namespace The_fifth_group_FinalAPI.DTOs
{
    public class CommodityDTO
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string ProductName { get; set; } = null!;
        public string ProductIntroduce { get; set; } = null!;
        public string Color { get; set; }
        public int Price { get; set; }
        public string ImageUrl { get; set; } = null!;


    }
}
