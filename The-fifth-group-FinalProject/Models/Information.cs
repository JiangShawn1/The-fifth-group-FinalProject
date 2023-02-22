using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalProject.Models
{
    public partial class Information
    {
        public Information()
        {
            RegistrationInformations = new HashSet<RegistrationInformation>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public bool Gender { get; set; }
        public string Address { get; set; } = null!;

        public virtual ICollection<RegistrationInformation> RegistrationInformations { get; set; }
    }
}
