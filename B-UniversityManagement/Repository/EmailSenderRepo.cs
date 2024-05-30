using B_UniversityManagement.IRepository;
using B_UniversityManagement.Models;
using MailKit.Net.Smtp;
using MimeKit;
using System.Text;

namespace B_UniversityManagement.Repository
{
    public class EmailSenderRepo : IEmailSenderRepo
    {
        public void SendEmail(User user , string pass , string college)
        {
            
            string senderEmail = "ahmedkhaledabas@yahoo.com";
            string appPassword = "fgqerfewpibgszpz";
            string senderName = college;
            string subject = $"{college} College Teams";
            string body = $@"
        <html>
        <head>
            <style>
                /* Define your CSS styles here */
            </style>
        </head>
        <body>
            <div class='container'>
            <h3>Dear <span>{user.FName + ' ' + user.LName}</span> ,</h3>
                <p>Your UserName : <strong>{user.UserName}</strong></p> 
                  <p>Your Password : <strong>{pass}</strong></p> 
               
            </div>
        </body>
        </ html > ";
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(senderName, senderEmail));
            message.To.Add(new MailboxAddress(user.FName, user.Email));
            message.Subject = subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = body; // You can also set TextBody for plain text

            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                // Connect to the SMTP server
                client.Connect("smtp.mail.yahoo.com", 465, true);

                // Authenticate with the SMTP server
                client.Authenticate(senderEmail, appPassword);

                // Send the email
                client.Send(message);

                // Disconnect from the SMTP server
                client.Disconnect(true);
            }
        }

        //public Task SendEmailAsync(string email, string subject, string message)
        //{
        //    string senderEmail = "ahmedkhaledabas@yahoo.com";
        //    string appPassword = "fgqerfewpibgszpz";

        //    var client = new SmtpClient("smtp.mail.yahoo.com", 465)
        //    {
        //        EnableSsl = true,
        //        Credentials = new NetworkCredential(senderEmail, appPassword)
        //    };

        //    return client.SendMailAsync(new MailMessage(
        //        from:senderEmail,
        //        to:email,
        //        subject,
        //        message));
        //}

       
            
    }
}
