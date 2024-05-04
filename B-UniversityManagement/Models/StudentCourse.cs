using B_UniversityManagement.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace B_UniversityManagement.Models
{
    public class StudentCourse : BaseProperties
    {
        public Guid StudentId { get; set; }
        public int CourseId { get; set; }
        public LevelYear Level { get; set; } = LevelYear.First;
        public decimal? Degree { get; set; }
        
    }
}
