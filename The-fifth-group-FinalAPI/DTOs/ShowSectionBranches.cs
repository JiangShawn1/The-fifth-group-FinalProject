using The_fifth_group_FinalAPI.Models;


namespace The_fifth_group_FinalAPI.DTOs
{
	public class ShowSectionBranches
	{
		public string SectionName { get; set; }


		public int SectionNameId { get; set; }
		public string BranchName { get; set; }
		public string SectionAdministrator { get; set; }
		public int AdministratorId { get; set; }

	}
}
