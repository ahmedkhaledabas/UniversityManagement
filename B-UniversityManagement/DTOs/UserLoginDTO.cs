using System.ComponentModel.DataAnnotations;

namespace B_UniversityManagement.DTOs
{
    public class UserLoginDTO
    {
        [Required]
        public string UserName { get; set; } = null!;
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; } = null!;

        public bool RememberMe { get; set; }
    }
}
