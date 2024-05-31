namespace B_UniversityManagement.Models
{
    public class Question
    {
        public string Id {  get; set; }
        public string Ques {  get; set; }
        public string Answer {  get; set; }
        public string Opt1 { get; set; }
        public string Opt2 {  get; set; }
        public string Opt3 { get; set; }
        public string Opt4 { get; set; }

        public string ProfessorId {  get; set; }
        public string CourseId { get; set; }



    }
}
