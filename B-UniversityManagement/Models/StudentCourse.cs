using B_UniversityManagement.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace B_UniversityManagement.Models
{
    public class StudentCourse : BaseProperties
    {
        public string StudentId { get; set; } = null!;
        public User User {  get; set; }
        public string CourseId { get; set; } = null!;
        public Course Course {  get; set; }
        //public LevelYear Level { get; set; } = LevelYear.First;
        public decimal? Degree { get; set; }
        
    }
}
