using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;


namespace The_fifth_group_FinalProject.DTOs
    {
    public class Forum_and_SectionDTO
        {
        public int Id { get; set; }
        public string? SectionName { get; set; }

        public int SectionNameId { get; set; }
        public string BranchName { get; set; } = null!;
        public string SectionAdministrator { get; set; } = null!;
        public int AdministratorId { get; set; }

        }
    }
