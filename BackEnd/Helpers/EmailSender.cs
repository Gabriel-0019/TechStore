using MailKit.Net.Smtp;
using MimeKit;
using System.Net.Mail;

namespace BackEnd.Helpers
{
    public class EmailSender
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPass;
        private readonly string _fromEmail;
        private readonly string _fromName;

        public EmailSender(IConfiguration configuration)
        {
            _smtpServer = configuration["Email:SmtpServer"]; 
            _smtpPort = int.Parse(configuration["Email:SmtpPort"] ?? "587");
            _smtpUser = configuration["Email:SmtpUser"];
            _smtpPass = configuration["Email:SmtpPass"];
            _fromEmail = configuration["Email:FromEmail"];
            _fromName = configuration["Email:FromName"];
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_fromName, _fromEmail));
            email.To.Add(new MailboxAddress("", toEmail));
            email.Subject = subject;

            var builder = new BodyBuilder
            {
                HtmlBody = message,
                TextBody = message 
            };

            email.Body = builder.ToMessageBody();

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                await client.ConnectAsync(_smtpServer, _smtpPort, MailKit.Security.SecureSocketOptions.StartTls);

                await client.AuthenticateAsync(_smtpUser, _smtpPass);

                await client.SendAsync(email);

                await client.DisconnectAsync(true);
            }
        }
    }
}
