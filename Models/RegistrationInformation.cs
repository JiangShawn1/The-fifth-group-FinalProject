using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalProject.Models
{
    public partial class RegistrationInformation
    {
        public int Id { get; set; }
        public int RegistrationId { get; set; }
        public int InformationId { get; set; }

        public virtual Information Information { get; set; } = null!;
        public virtual Registration Registration { get; set; } = null!;
    }
}
