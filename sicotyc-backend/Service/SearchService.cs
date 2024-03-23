using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
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
                case "LOOKUPCODEGROUPS":
                    return await SearchLookupCodeGroupsAsync(searchTerm);
                case "LOOKUPCODES":
                    return await SearchLookupCodesAsync(searchTerm);
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

        public async Task<IEnumerable<SearchResultDto>> SearchLookupCodeGroupsAsync(string searchTerm)
        {
            LookupCodeGroupParameters lookupCodeGroupParameters = new LookupCodeGroupParameters();
            lookupCodeGroupParameters.SearchTerm = searchTerm;
            lookupCodeGroupParameters._pageSize = 1000;

            var lookupCodeGroupsFromDB = await _repository.LookupCodeGroup.GetAllLookupCodeGroupsAsync(lookupCodeGroupParameters, trackChanges: false);

            var searchesDto = _mapper.Map<IEnumerable<SearchResultDto>>(lookupCodeGroupsFromDB);
            searchesDto.ForEach(x => { x.Entity = "LookupCodeGroups"; });

            return searchesDto;
            
        }

        public async Task<IEnumerable<SearchResultDto>> SearchLookupCodesAsync(string searchTerm)
        {
            string[] search = searchTerm.Split("|");
            Guid lookupCodeGroupId = new Guid(search[0].ToString());
            string term = search[1].ToString();

            LookupCodeParameters lookupCodeParameters = new LookupCodeParameters();
            lookupCodeParameters.SearchTerm = term;
            lookupCodeParameters._pageSize = 1000;            

            var lookupCodesFromDB = await _repository.LookupCode.GetLookupCodesAsync(lookupCodeGroupId, lookupCodeParameters, trackChanges: false);

            var searchesDto = _mapper.Map<IEnumerable<SearchResultDto>>(lookupCodesFromDB);
            searchesDto.ForEach(x => { x.Entity = "LookupCodes"; });

            return searchesDto;

        }
    }
}
