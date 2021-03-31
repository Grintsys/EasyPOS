using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;
using Volo.Abp.Security.Encryption;

namespace Grintsys.EasyPOS.Email
{
    public class EmailService : ITransientDependency
    {
        private readonly IEmailSender _emailSender;
        public IStringEncryptionService _encryptionService { get; set; }

        public EmailService(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task SendEmailAsync()
        {
            var encryptedGmailPassword = _encryptionService.Encrypt("Apache17");
            await _emailSender.SendAsync("recipient-email-here", "Email subject", "This is the email body...");
        }
    }
}
