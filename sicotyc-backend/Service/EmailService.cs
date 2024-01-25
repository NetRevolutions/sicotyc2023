using Contracts;
using Entities.Models;
using FluentEmail.Core;
using FluentEmail.Core.Models;
using Service.Contracts;


namespace Service
{
    public class EmailService : IEmailService
    {
        private readonly IFluentEmail _fluentEmail;
        private readonly ILoggerManager _logger;

        public EmailService(IFluentEmail fluentEmail, ILoggerManager logger)
        {
            _fluentEmail = fluentEmail ?? throw new ArgumentNullException(nameof(fluentEmail));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<bool> SendMailAsync(EmailMetadata emailMetadata)
        {
            List<Address> toAdressList = new List<Address>();
            List<Address> toCCList = new List<Address>();
            List<Address> toBccList = new List<Address>();

            if (emailMetadata.ToAddress?.Count > 0)
            {   
                foreach (var emailItem in emailMetadata.ToAddress)
                {
                    Address address = new Address { EmailAddress = emailItem.Email, Name = emailItem.Name };
                    toAdressList.Add(address);
                }
            }

            if (emailMetadata.ToCC?.Count > 0)
            {

                foreach (var emailItem in emailMetadata.ToCC)
                {
                    Address address = new Address { EmailAddress = emailItem.Email, Name = emailItem.Name };
                    toCCList.Add(address);
                }
            }

            if (emailMetadata.ToBcc?.Count > 0)
            {

                foreach (var emailItem in emailMetadata.ToBcc)
                {
                    Address address = new Address { EmailAddress = emailItem.Email, Name = emailItem.Name };
                    toBccList.Add(address);
                }
            }

            var result = await _fluentEmail.To(toAdressList)
                .CC(toCCList)
                .BCC(toBccList)
                .Subject(emailMetadata.Subject)
                .Body(emailMetadata.Body, emailMetadata.IsHtml)               
                .SendAsync();

            if (result.Successful)
            {
                return true;
            }
            else
            {
                _logger.LogError($"Se produjo el siguiente error al enviar correo: {result.ErrorMessages}");
                return false;
            }
        }
    }
}
