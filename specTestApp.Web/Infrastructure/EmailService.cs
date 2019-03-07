using Microsoft.AspNet.Identity;
using specTestApp.Services;
using System.Net.Mail;
using System.Threading.Tasks;

namespace specTestApp.Web.Infrastructure
{
    public class EmailService : IIdentityMessageService
    {
        private IConfigurationProvider _configurationProvider;

        public EmailService(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public Task SendAsync(IdentityMessage message)
        {
            var from = _configurationProvider.GetConfig("EmailLogin");
            var pass = _configurationProvider.GetConfig("EmailPassword");
            var server = _configurationProvider.GetConfig("SmtpServer");
            var port = _configurationProvider.GetConfig("SmtpPort", 25);

            SmtpClient client = new SmtpClient(server, port);

            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(from, pass);
            client.EnableSsl = true;

            var mail = new MailMessage(from, message.Destination);
            mail.Subject = message.Subject;
            mail.Body = message.Body;
            mail.IsBodyHtml = true;

            return client.SendMailAsync(mail);
        }
    }
}