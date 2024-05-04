﻿using B_UniversityManagement.Enums;

namespace B_UniversityManagement.Models
{
    public class Employee : User
    {
        public decimal? EmpSalary { get; set; }
        public int CollegeId { get; set; }
        public College College { get; set; } = null!;
       
    }
}
