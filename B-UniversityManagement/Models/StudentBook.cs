using Microsoft.EntityFrameworkCore;

namespace B_UniversityManagement.Models
{
    public class StudentBook
    {
        public Guid StudentId { get; set; } 
        public int BookId { get; set; } 
        public DateTime Start {  get; set; }
        public DateTime End { get; set; }

    }
}
