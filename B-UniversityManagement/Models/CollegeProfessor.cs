using B_UniversityManagement.Enums;
using Microsoft.EntityFrameworkCore;

namespace B_UniversityManagement.Models
{
    public class CollegeProfessor
    {
        public int CollegeId { get; set; } 
        public Guid ProfessorId { get; set; } 
        public AcademicRank Rank { get; set; } = AcademicRank.Professor;
    }
}
