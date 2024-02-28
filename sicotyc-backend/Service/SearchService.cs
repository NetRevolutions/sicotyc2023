using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.RequestFeatures;
using FluentEmail.Core;
using Service.Contracts;

namespace Service
{
    public class SearchService : ISearchService
    {        
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public SearchService(IRepositoryManager repository, IMapper mapper)
        {            
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SearchResultDto>> SearchAllAsync(string searchTerm)
        {
            return await SearchUsersAsync(searchTerm);

            // TODO: Cuando se requiera buscar en otros modelos se hace join de los resultados.
        }

        public async Task<IEnumerable<SearchResultDto>> SearchByCollectionAsync(string collection, string searchTerm)
        {
            switch (collection.Trim().ToUpper())
            {
                case "USERS":
                    return await SearchUsersAsync(searchTerm);                    
                case "TRANSPORTS":                    
                    throw new NotImplementedException($"La coleccion {collection} aun no se encuentra implementada");                   
                    
                default:
                    throw new ArgumentException("Coleccion no valida", nameof(collection));
                       
            }
        }

        public async Task<IEnumerable<SearchResultDto>> SearchUsersAsync(string searchTerm)
        {
            UserParameters userParameters = new UserParameters();
            userParameters.SearchTerm = searchTerm;
            userParameters._pageSize = 1000;

            var usersFromDB = await _repository.AuthenticationManager.GetUsersAsync(userParameters, trackChanges: false);

            var searchesDto = _mapper.Map<IEnumerable<SearchResultDto>>(usersFromDB);
            searchesDto.ForEach(x => { x.Entity = "Users"; });
            
            return searchesDto;
            
        }
    }
}
