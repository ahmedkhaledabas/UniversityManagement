using B_UniversityManagement.Models;

namespace B_UniversityManagement.IRepository
{
    public interface IEmailSenderRepo
    {
        //Task SendEmailAsync(string email , string subject , string message);
        void SendEmail(User user, string pass, string college);
    }
}
