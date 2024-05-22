using B_UniversityManagement.Enums;
using Microsoft.AspNetCore.Identity;

namespace B_UniversityManagement.Models
{
    public class User  : IdentityUser
    {
        //public string Id { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string FName { get; set; } = null!;
        public string? LName { get; set; }
        //public string Email { get; set; } = null!;
        //public string Password { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string? Address { get; set; }
        public Gender Gender { get; set; }
        public string? Img { get; set; }
    }
}
