namespace B_UniversityManagement.Models
{
    public class Library : BaseProperties
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Img { get; set; }
        public List<Book>? Books {  get; set; }
        public string CollegeId { get; set; }
        public College College { get; set; }=null!;
       
    }
}
