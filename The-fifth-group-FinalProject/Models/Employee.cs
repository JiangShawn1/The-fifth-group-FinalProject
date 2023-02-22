﻿using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalProject.Models
{
    public partial class Employee
    {
        public Employee()
        {
            ChatContents = new HashSet<ChatContent>();
        }

        public int Id { get; set; }
        public string Account { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Title { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public int Permission { get; set; }

        public virtual ICollection<ChatContent> ChatContents { get; set; }
    }
}
