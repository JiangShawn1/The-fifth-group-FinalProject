namespace The_fifth_group_FinalAPI.DTOs
{
    public class CartItemsDTO
    {
        public int Id { get; set; }
        public int Member_Id { get; set; }
        public int Product_Id { get; set; }
        public int Qty { get; set; }
        public string ProductName { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public int Price { get; set; }
        public string ProductIntroduce { get; set; }
        public string ImageUrl { get; set; }
    }
}
