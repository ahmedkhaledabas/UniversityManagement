using B_UniversityManagement.Enums;

namespace B_UniversityManagement.Models
{
    public class Employee : User
    {
        public decimal? EmpSalary { get; set; }
        public string CollegeId { get; set; } = null!;
        public College College { get; set; } = null!;
       
    }
}
