﻿using System.ComponentModel.DataAnnotations;

namespace B_UniversityManagement.Models
{
    public class BaseProperties
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
