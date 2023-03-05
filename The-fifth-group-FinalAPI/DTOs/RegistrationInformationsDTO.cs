namespace The_fifth_group_FinalAPI.DTOs
{
	public class RegistrationInformationsDTO
	{
		public int Id { get; set; }
		public int RegistrationId { get; set; }
		public int InformationId { get; set; }
		public int MemberId { get; set; }
		public string? Member { get; set; }
		public int ContestCategoryId { get; set; }
		public string? CategoryName { get; set; }
		public int ContestId { get; set; }
		public DateTime ContestDate { get; set; }
		public string? ContestName { get; set; }
		public bool PaymentStatus { get; set; }
		public string? Name { get; set; }
		public string? Phone { get; set; }
		public bool Gender { get; set; }
		public string? Address { get; set; }

	}
}
