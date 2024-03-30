using Contracts;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Sicotyc.ActionFilters;

namespace Sicotyc.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;
        private readonly IAuthenticationManager _authManager;
        private readonly ILoggerManager _logger;

        public SearchController(ISearchService searchService, ILoggerManager logger, IAuthenticationManager authManager)
        {
            _searchService = searchService;
            _logger = logger;
            _authManager = authManager;
        }

        [HttpGet("all/{searchTerm}")]
        [ServiceFilter(typeof(ValidationTokenFilter))]
        public async Task<IActionResult> SearchAll(string searchTerm) {
            try
            {
                var result = await _searchService.SearchAllAsync(searchTerm);
                return Ok(new { result });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Hubo un error al tratar de realizar la busqueda, aca el detalle: {ex.Message}");
                return BadRequest("Hubo un error al tratar de realizar la busqueda");
            }            
        }

        [HttpGet("collection/{collection}/{searchTerm}")]
        [ServiceFilter(typeof(ValidationTokenFilter))]
        public async Task<IActionResult> SearchByCollection(string collection, string searchTerm) 
        {
            try
            {
                var result = await _searchService.SearchByCollectionAsync(collection, searchTerm);
                return Ok(new { result });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Hubo un error al tratar de realizar la busqueda por coleccion, aca el detalle: {ex.Message}");
                return BadRequest("Hubo un error al tratar de realizar la busqueda por coleccion");
            }            
        }

    }
}
