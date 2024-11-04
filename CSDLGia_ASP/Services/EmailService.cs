using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using System.Net.Mail;

namespace CSDLGia_ASP.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string recipientEmail, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(
                _configuration["SmtpSettings:SenderName"],
                _configuration["SmtpSettings:SenderEmail"]));

            emailMessage.To.Add(new MailboxAddress("", recipientEmail));
            emailMessage.Subject = subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = $@"
                <html>
                    <body>
                        <h1 style='color: #333333;'>Hệ thống CSDL về giá!</h1>
                        <p>Xin chào!!!</p>
                        <p>{message}.</p>
                        <p>---</p>
                        <p>Thư trên được gửi tự động. Vui lòng không reply lại. Xin cảm ơn!</p>
                    </body>
                </html>";
            emailMessage.Body = builder.ToMessageBody();

            using var client = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                // Bỏ qua xác thực chứng chỉ trong môi trường phát triển
                client.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                await client.ConnectAsync(
                    _configuration["SmtpSettings:Server"],
                    int.Parse(_configuration["SmtpSettings:Port"]),
                    SecureSocketOptions.StartTls);

                await client.AuthenticateAsync(
                    _configuration["SmtpSettings:Username"],
                    _configuration["SmtpSettings:Password"]);

                await client.SendAsync(emailMessage);
            }
            catch
            {
                throw;
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }
    }
}
