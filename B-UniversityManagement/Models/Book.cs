using System.ComponentModel.DataAnnotations.Schema;

namespace B_UniversityManagement.Models
{
    public class Book : BaseProperties
    {
        public string Name { get; set; } = null !;
        public string? Description { get; set; }
        public string? AuthorName { get; set; }
        public string? Img { get; set; }
        public string LibraryId { get; set; } = null!;
        public Library Library { get; set; } = null!;
        //public List<User>? Students { get; set; }
        public ICollection<StudentBook> StudentBooks { get; set; } = new List<StudentBook>();
            


    }
}
