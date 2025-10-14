// Services/EmailService.cs
using System.Net;
using System.Net.Mail;

namespace InvoiceApp.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            // For development, just log the email
            // In production, use a real email service like SendGrid, AWS SES, etc.

            var smtpHost = _configuration["Email:SmtpHost"];
            var smtpPort = int.Parse(_configuration["Email:SmtpPort"] ?? "587");
            var smtpUser = _configuration["Email:SmtpUser"];
            var smtpPass = _configuration["Email:SmtpPass"];
            var fromEmail = _configuration["Email:FromEmail"];
            var fromName = _configuration["Email:FromName"];

            if (string.IsNullOrEmpty(smtpHost))
            {
                // Development mode - just log to console
                Console.WriteLine("=== EMAIL ===");
                Console.WriteLine($"To: {email}");
                Console.WriteLine($"Subject: {subject}");
                Console.WriteLine($"Message: {message}");
                Console.WriteLine("=============");
                return;
            }

            try
            {
                using var client = new SmtpClient(smtpHost, smtpPort)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(smtpUser, smtpPass)
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(fromEmail, fromName),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(email);

                await client.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error sending email: {ex.Message}");
                throw;
            }
        }
    }
}