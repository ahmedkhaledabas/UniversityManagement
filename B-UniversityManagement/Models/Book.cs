using System.ComponentModel.DataAnnotations.Schema;

namespace B_UniversityManagement.Models
{
    public class Book : BaseProperties
    {
        public string Name { get; set; } = null !;
        public string? Description { get; set; }
        public string? AuthorName { get; set; }
        public string? Img { get; set; }
        public int LibraryId { get; set; } 
        public Library Library { get; set; } = null!;
        public List<Student>? Students { get; set; }
            


    }
}
