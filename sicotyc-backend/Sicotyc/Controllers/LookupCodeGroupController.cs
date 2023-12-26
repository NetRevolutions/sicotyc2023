using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Sicotyc.ActionFilters;
using Sicotyc.ModelBinders;

namespace Sicotyc.Controllers
{
    [Route("api/lookupCodeGroups")]
    [ApiController]
    public class LookupCodeGroupController : ControllerBase
    {
        private readonly IServiceManager _service;
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public LookupCodeGroupController(IServiceManager service, IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _service = service;
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        //[HttpGet]
        //[HttpGet(Name = "GetLookupCodeGroups"), Authorize] // Con esto implementamos autorizacion a los endpoints que deseamos
        [HttpGet(Name = "GetLookupCodeGroups"), Authorize(Roles = "Manager,Administrator")] // Con esto indicamos que solo un determinado rol tiene acceso
        public async Task<IActionResult> GetLookupCodeGroups() // Mejorar para cambiarlo por ActionResult
        {
            // throw new Exception("Exception"); // Usado para pruebas
            var lookupCodeGroups = await _repository.LookupCodeGroup.GetAllLookupCodeGroupsAsync(trackChanges: false);
            var lookupCodeGroupsDto = _mapper.Map<IEnumerable<LookupCodeGroupDto>>(lookupCodeGroups);
            return Ok(lookupCodeGroupsDto);
        }

        [HttpGet("{id:guid}", Name ="LookupCodeGroupById")]
        public async Task<IActionResult> GetLookupCodeGroup(Guid id)
        {
            var lookupCodeGroup = await _repository.LookupCodeGroup.GetLookupCodeGroupAsync(id, trackChanges: false); 
            
            if (lookupCodeGroup == null)
            {
                _logger.LogError($"El LookupCodeGroup con id {id} no existe en la base de datos.");
                return NotFound();
            }
            else
            {
                var lookupCodeGroupDto = _mapper.Map<LookupCodeGroupDto>(lookupCodeGroup);
                return Ok(lookupCodeGroupDto);
            }           
        }

        [HttpGet("collection/({ids})", Name = "LookupCodeGroupCollection")]
        public async Task<IActionResult> GetLookupCodeGroupCollection(
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parametro ids es nulo");
                return BadRequest("Parametro ids es nulo");
            }

            var lookupCodeGroupEntities = await _repository.LookupCodeGroup.GetByIdsAsync(ids, trackChanges: false);
            
            if (ids.Count() != lookupCodeGroupEntities.Count()) {
                _logger.LogError("Algunos de los Ids de la coleccion no son validos");
                return NotFound();
            }

            var lookupCodeGroupstoReturn = _mapper.Map<IEnumerable<LookupCodeGroupDto>>(lookupCodeGroupEntities);
            return Ok(lookupCodeGroupstoReturn);

        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateLookupCodeGroup([FromBody] LookupCodeGroupForCreationDto lookupCodeGroup) 
        {
            var lookupCodeGroupEntity = _mapper.Map<LookupCodeGroup>(lookupCodeGroup);

            if (lookupCodeGroupEntity.LookupCodes?.Count > 0)
            {
                foreach (var item in lookupCodeGroupEntity.LookupCodes)
                {
                    item.LookupCodeGroupId = lookupCodeGroupEntity.Id;
                }
            }

            _repository.LookupCodeGroup.CreateLookupCodeGroup(lookupCodeGroupEntity);
            await _repository.SaveAsync();

            var lookupCodeGroupToReturn = _mapper.Map<LookupCodeGroupDto>(lookupCodeGroupEntity);

            return CreatedAtRoute("LookupCodeGroupById", new { id = lookupCodeGroupToReturn.Id }, lookupCodeGroupToReturn);
        }

        [HttpPost("collection")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateLookupCodeGroupCollection([FromBody] IEnumerable<LookupCodeGroupForCreationDto> lookupCodeGroupCollection) 
        { 
            var lookupCodeGroupEntities = _mapper.Map<IEnumerable<LookupCodeGroup>>(lookupCodeGroupCollection);
            foreach (var lookupCodeGroup in lookupCodeGroupEntities) 
            {
                _repository.LookupCodeGroup.CreateLookupCodeGroup(lookupCodeGroup);
            }

            await _repository.SaveAsync();

            var lookupCodeGroupCollectionToReturn = _mapper.Map<IEnumerable<LookupCodeGroupDto>>(lookupCodeGroupEntities);
            var ids = string.Join(",", lookupCodeGroupCollectionToReturn.Select(c => c.Id));

            return CreatedAtRoute("LookupCodeGroupCollection", new { ids }, lookupCodeGroupCollectionToReturn);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateLookupCodeGroup(Guid id, [FromBody] LookupCodeGroupForUpdateDto lookupCodeGroup) 
        { 
            var lookupCodeGroupEntity = await _repository.LookupCodeGroup.GetLookupCodeGroupAsync(id, trackChanges: true);
            if (lookupCodeGroupEntity == null)
            {
                _logger.LogInfo($"El LookupCodeGroup con id: {id} no existe en la base de datos.");
                return NotFound();
            }

            if (lookupCodeGroup.LookupCodes?.Count() > 0)
            {
            // Aca grabamos lookupCodeGroup (update) y lookupCode (create) por separado.
            // ========================================================================================================
            // lookupCodeGroup - begin
            // ========================================================================================================
                var lookupCodesTemp = lookupCodeGroup.LookupCodes;

                _mapper.Map(lookupCodeGroup, lookupCodeGroupEntity);

                lookupCodeGroupEntity.LookupCodes = null;
            // ========================================================================================================
            // lookupCodeGroup - end
            // ========================================================================================================


            // ========================================================================================================
            // lookupCode section - begin
            // ========================================================================================================
                int lastLookupCodeOrder = await _repository.LookupCode.GetLastLookupCodeOrderAsync(lookupCodeGroupEntity.Id);
                lastLookupCodeOrder++;

                foreach (var item in lookupCodesTemp)
                {
                    var lookupCodeEntity = _mapper.Map<LookupCode>(item);
                    lookupCodeEntity.LookupCodeGroupId = lookupCodeGroupEntity.Id;
                    lookupCodeEntity.LookupCodeOrder = lastLookupCodeOrder;
                    lookupCodeEntity.CreatedBy = lookupCodeGroupEntity.CreatedBy;
                    lastLookupCodeOrder++;

                    _repository.LookupCode.CreateLookupCodeForLookupCodeGroup(lookupCodeGroupEntity.Id, lookupCodeEntity);
                }
            // ========================================================================================================
            // lookupCode section - end
            // ========================================================================================================
            }
            else { 
                _mapper.Map(lookupCodeGroup, lookupCodeGroupEntity);                
            }

            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLookupCodeGroup(Guid id)
        {
            var lookupCodeGroup = await _repository.LookupCodeGroup.GetLookupCodeGroupAsync(id, trackChanges: false);
            if (lookupCodeGroup == null)
            {
                _logger.LogInfo($"LookupCodeGroup con id {id} no existe en la base de datos.");
                return NotFound();
            }

            _repository.LookupCodeGroup.DeleteLookupCodeGroup(lookupCodeGroup);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
