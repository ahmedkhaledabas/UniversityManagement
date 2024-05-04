using B_UniversityManagement.Enums;
using System.ComponentModel.DataAnnotations;

namespace B_UniversityManagement.DTOs
{
    public class StudentDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "Email Must Contain @gmail Or yahoo.com")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$", ErrorMessage = "Password Must at least 8 Char Contain Special Char , Number , Upper,Lower Char")]
        public string Password { get; set; }
        [Required]
        [RegularExpression("^(\\+201|01|00201)[0-2,5]{1}[0-9]{8}", ErrorMessage = "Invaild Number")]
        public string Phone { get; set; }
        [Required]
        public DateOnly DateOfBirth { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public Gender Gender { get; set; }
    }
}
