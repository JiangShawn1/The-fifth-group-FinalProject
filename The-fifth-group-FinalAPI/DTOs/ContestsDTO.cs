using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace The_fifth_group_FinalAPI.DTOs
{
	public class ContestsDTO
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? SupplierName { get; set; } 
		public DateTime ContestDate { get; set; }
		
		public DateTime RegistrationDeadline { get; set; }
		
		public string? Area { get; set; }

		public string? Location { get; set; } 
		
		public string? MapUrl { get; set; }
		public List<string>? CategoryName { get; set; }
		public List<int>? Distance { get; set; }
		public List<int>? Quota { get; set; }
		public List<int>? EnterFee { get; set; }		
		 
		public string? Detail { get; set; }

		public bool Review { get; set; }
	}
}
