using B_UniversityManagement.Enums;
using Microsoft.EntityFrameworkCore;

namespace B_UniversityManagement.Models
{
    public class CollegeProfessor
    {
        public string CollegeId { get; set; } = null!;
        public string ProfessorId { get; set; } = null!;
        public AcademicRank Rank { get; set; } = AcademicRank.Professor;
    }
}
