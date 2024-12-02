using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailService
{
    private readonly string _smtpHost = "smtp.gmail.com"; // Replace with your SMTP server
    private readonly int _smtpPort = 587; // Common port for TLS
    private readonly string _smtpUser = "naveedhassan0710@gmail.com"; // Your email address
    private readonly string _smtpPass = "pfvz rjou gjkw yfnq"; // Your email password

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        try
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(toEmail));
            message.From = new MailAddress(_smtpUser, "HMS System");
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using (var client = new SmtpClient(_smtpHost, _smtpPort))
            {
                client.Credentials = new NetworkCredential(_smtpUser, _smtpPass);
                client.EnableSsl = true;
                await client.SendMailAsync(message);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
        }
    }

}
