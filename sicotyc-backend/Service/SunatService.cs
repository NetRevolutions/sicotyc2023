using AutoMapper;
using Contracts;
using Entities.Models;
using Service.Contracts;

namespace Service
{
    public class SunatService : ISunatService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public SunatService(IRepositoryManager repository, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<SunatResponse> GetSunatCompanyDataAsync(string ruc)
        {
            throw new NotImplementedException();
        }
    }
}
