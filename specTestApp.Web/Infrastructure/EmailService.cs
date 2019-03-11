using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using specTestApp.Services;
using System.Net.Mail;
using System.Threading.Tasks;

namespace specTestApp.Web.Infrastructure
{
    public class EmailService : IIdentityMessageService
    {
        private readonly IConfigurationProvider _configurationProvider;

        public EmailService(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public Task SendAsync(IdentityMessage message)
        {
            var client = GetSmtpClient();
            var from = _configurationProvider.GetConfig("EmailLogin");

            var mail = new MailMessage(from, message.Destination);
            mail.Subject = message.Subject;
            mail.Body = message.Body;
            mail.IsBodyHtml = true;

            return client.SendMailAsync(mail);
        }

        public async Task SendAsync(string subject, string message, List<string> sendToEmails)
        {
            var client = GetSmtpClient();
            var from = _configurationProvider.GetConfig("EmailLogin");

            foreach (var email in sendToEmails)
            {
                var mail = new MailMessage(from, email)
                {
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };

                await client.SendMailAsync(mail);
            }
        }

        private SmtpClient GetSmtpClient()
        {
            var from = _configurationProvider.GetConfig("EmailLogin");
            var pass = _configurationProvider.GetConfig("EmailPassword");
            var server = _configurationProvider.GetConfig("SmtpServer");
            var port = _configurationProvider.GetConfig("SmtpPort", 25);

            var client = new SmtpClient(server, port);

            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(from, pass);
            client.EnableSsl = true;

            return client;
        }
    }
}