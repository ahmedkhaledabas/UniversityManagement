namespace B_UniversityManagement.Models
{
    public class Fee : BaseProperties
    {
        public bool Pay { get; set; } = false;

        public Guid StudentId { get; set; } 
        public Student Student { get; set; } = null!;
    }
}
