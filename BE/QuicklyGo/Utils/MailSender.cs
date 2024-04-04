using System.Net;
using System.Net.Mail;

namespace QuicklyGo.Utils
{
    public class MailSender
    {
        public static async Task<bool> SendMailAsync(string receiverEmail, string subject, string body)
        {
            // Create SMTP client
            var _defaultEmail = ConfigurationManager.AppSetting["Email:DefaultEmail"];
            var _defaultEmailPassword = ConfigurationManager.AppSetting["Email:DefaultEmailPassword"];
            var _client = new SmtpClient("smtp.gmail.com");
            _client.Port = 587;
            _client.Credentials = new NetworkCredential(_defaultEmail, _defaultEmailPassword);
            _client.EnableSsl = true;

            // Tạo nội dung Email
            MailMessage message = new MailMessage(
                from: _defaultEmail,
                to: receiverEmail,
                subject: subject,
                body: body
            );
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            message.ReplyToList.Add(new MailAddress(_defaultEmail));
            message.Sender = new MailAddress(_defaultEmail);

            try
            {
                await _client.SendMailAsync(message);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
