using Domain.Repositories.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Shared.Helpers;


namespace Infrastructure.Services;



public class EmailService : IEmailService
{
    public async Task SendEmailAsync(string recipient, string subject, string body)
    {
        await SendEmailAsync(new List<string> { recipient }, subject, body);
    }

    public async Task SendEmailAsync(List<string> recipients, string subject, string body)
    {
        Appsettings appsettings = Appsettings.Instance;
        var email = new MimeMessage();
        email.Sender = MailboxAddress.Parse(appsettings.GetValue("MailSettings:Email"));

        foreach (var recipient in recipients)
            email.To.Add(MailboxAddress.Parse(recipient));

        email.Subject = subject;

        var builder = new BodyBuilder();
        builder.HtmlBody = body;
        email.Body = builder.ToMessageBody();

        using (var smtp = new SmtpClient())
        {
            await smtp.ConnectAsync(appsettings.GetValue("MailSettings:Host"), ConversionHelper.ConvertTo<int>(appsettings.GetValue("MailSettings:Port")), SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(appsettings.GetValue("MailSettings:Email"), appsettings.GetValue("MailSettings:Password"));
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
