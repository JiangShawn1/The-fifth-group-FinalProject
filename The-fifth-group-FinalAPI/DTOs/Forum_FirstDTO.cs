using System.Collections.Generic;
using The_fifth_group_FinalAPI.Models;
using System.Linq;

namespace The_fifth_group_FinalAPI.DTOs
{
    public class Forum_FirstDTO
    {
        public int Id { get; set; }
        public string? SectionName { get; set; }
        public int? SectionNameId { get; set; }
        public string? BranchName { get; set; }
        public int? AdministratorId { get; set; }

    }
}
