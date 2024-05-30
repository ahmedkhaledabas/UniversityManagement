using Microsoft.EntityFrameworkCore;

namespace B_UniversityManagement.Models
{
    public class StudentBook
    {
        public string StudentId { get; set; } = null!;
        public User User { get; set; }
        public string BookId { get; set; } = null!; 
        public Book Book { get; set; }

    }
}
