using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using NetCoreStartProject.Contracts.V1.Requests;
using NetCoreStartProject.Options;
using System.IO;
using System.Threading.Tasks;

namespace NetCoreStartProject.Services
{
    public class MailService : IMailService , IEmailSender
    {
        private readonly MailSettings _mailSettings;
        public MailService(MailSettings mailSettings)
        {
            _mailSettings = mailSettings;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var mail = new MimeMessage();
            mail.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            mail.To.Add(MailboxAddress.Parse(email));
            mail.Subject = subject;
            var builder = new BodyBuilder();
            
            builder.HtmlBody = htmlMessage;
            mail.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(mail);
            smtp.Disconnect(true);
        }
    }
}
