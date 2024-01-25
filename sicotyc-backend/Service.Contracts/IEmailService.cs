using Entities.Models;

namespace Service.Contracts
{
    public interface IEmailService
    {
        Task<bool> SendMailAsync(EmailMetadata emailMetadata);
    }
}
