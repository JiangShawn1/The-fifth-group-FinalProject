namespace The_fifth_group_FinalAPI.DTOs
{
	public class MemberDTO
	{
		public int MemberId { get; set; }
		public string? Name { get; set; }
		public string? Account { get; set; }
		public string? Password { get; set; }
		public string? Phone { get; set; }
		public string? Mail { get; set; }
		public bool Subscription { get; set; }
		public bool IsConfirmed { get; set; }
		public bool Freeze { get; set; }
		public int State { get; set; }
	}
}
