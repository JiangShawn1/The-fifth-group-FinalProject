namespace The_fifth_group_FinalAPI.DTOs
{
	public class ProductsDTO
	{
		public int Id { get; set; }

		public int Brand_Id { get; set; }
		public string ProductName { get; set; }

		public int Price { get; set; }

		public int Color_Id { get; set; }

		public string Brand { get; set; }

		public string Color { get; set; }
		public string ProductIntroduce { get; set; }

		public string ImageUrl { get; set; }
	}
}
