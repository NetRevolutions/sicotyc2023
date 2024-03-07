using Entities.Models;

namespace Service.Contracts
{
    public interface ISunatService
    {
        Task<SunatResponse> GetSunatCompanyDataAsync(string ruc);
    }
}
