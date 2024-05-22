namespace B_UniversityManagement.Models
{
    public class Fee : BaseProperties
    {
        public bool Pay { get; set; } = false;

        public string StudentId { get; set; } = null!;
        public Student Student { get; set; } = null!;
    }
}
