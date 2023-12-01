using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Sicotyc.ModelBinders;
using System.Net.WebSockets;

namespace Sicotyc.Controllers
{
    //[Route("api/[controller]")]
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

        [HttpGet]
        public IActionResult GetLookupCodeGroups() // Mejorar para cambiarlo por ActionResult
        {
            // throw new Exception("Exception"); // Usado para pruebas
            var lookupCodeGroups = _service.LookupCodeGroupService.GetAllLookupCodeGroups(trackChanges: false);
            return Ok(lookupCodeGroups);
        }

        [HttpGet("{id:guid}", Name ="LookupCodeGroupById")]
        public IActionResult GetLookupCodeGroup(Guid id)
        {
            var lookupCodeGroup = _service.LookupCodeGroupService.GetLookupCodeGroup(id, trackChanges: false);
            
            return Ok(lookupCodeGroup);
        }

        [HttpGet("collection/({ids})", Name = "LookupCodeGroupCollection")]
        public IActionResult GetLookupCodeGroupCollection(
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parametro ids es nulo");
                return BadRequest("Parametro ids es nulo");
            }

            var lookupCodeGroupEntities = _repository.LookupCodeGroup.GetByIds(ids, trackChanges: false);
            
            if (ids.Count() != lookupCodeGroupEntities.Count()) {
                _logger.LogError("Algunos de los Ids de la coleccion no son validos");
                return NotFound();
            }

            var lookupCodeGroupstoReturn = _mapper.Map<IEnumerable<LookupCodeGroupDto>>(lookupCodeGroupEntities);
            return Ok(lookupCodeGroupstoReturn);

        }

        [HttpPost]
        public IActionResult CreateLookupCodeGroup([FromBody] LookupCodeGroupForCreationDto lookupCodeGroup) {
            if (lookupCodeGroup == null)
            {
                _logger.LogError("El objeto LookupCodeGroupForCreationDto enviado del cliente es nulo");
                return BadRequest("El objeto LookupCodeGroupForCreationDto es nulo");
            }

            var lookupCodeGroupEntity = _mapper.Map<LookupCodeGroup>(lookupCodeGroup);

            if (lookupCodeGroupEntity.LookupCodes?.Count > 0)
            {
                foreach (var item in lookupCodeGroupEntity.LookupCodes)
                {
                    item.LookupCodeGroupId = lookupCodeGroupEntity.Id;
                }
            }

            _repository.LookupCodeGroup.CreateLookupCodeGroup(lookupCodeGroupEntity);
            _repository.Save();

            var lookupCodeGroupToReturn = _mapper.Map<LookupCodeGroupDto>(lookupCodeGroupEntity);

            return CreatedAtRoute("LookupCodeGroupById", new { id = lookupCodeGroupToReturn.Id }, lookupCodeGroupToReturn);
        }

        [HttpPost("collection")]
        public IActionResult CreateLookupCodeGroupCollection([FromBody] IEnumerable<LookupCodeGroupForCreationDto> lookupCodeGroupCollection) 
        { 
            if (lookupCodeGroupCollection == null)
            {
                _logger.LogError("La coleccion de LookupCodeGroups enviados desde el cliente es nula");
                return BadRequest("La coleccion de LookupCodeGroups es nula");
            }

            var lookupCodeGroupEntities = _mapper.Map<IEnumerable<LookupCodeGroup>>(lookupCodeGroupCollection);
            foreach (var lookupCodeGroup in lookupCodeGroupEntities) 
            {
                _repository.LookupCodeGroup.CreateLookupCodeGroup(lookupCodeGroup);
            }

            _repository.Save();

            var lookupCodeGroupCollectionToReturn = _mapper.Map<IEnumerable<LookupCodeGroupDto>>(lookupCodeGroupEntities);
            var ids = string.Join(",", lookupCodeGroupCollectionToReturn.Select(c => c.Id));

            return CreatedAtRoute("LookupCodeGroupCollection", new { ids }, lookupCodeGroupCollectionToReturn);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLookupCodeGroup(Guid id, [FromBody] LookupCodeGroupForUpdateDto lookupCodeGroup) 
        { 
            if (lookupCodeGroup == null)
            {
                _logger.LogError("El objeto LookupCodeGroup enviado por el cliente es nulo.");
                return BadRequest("El objeto LookupCodeGroup es nulo.");
            }

            var lookupCodeGroupEntity = _repository.LookupCodeGroup.GetLookupCodeGroup(id, trackChanges: true);
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
                int lastLookupCodeOrder = _repository.LookupCode.GetLastLookupCodeOrder(lookupCodeGroupEntity.Id);
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

            _repository.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLookupCodeGroup(Guid id)
        {
            var lookupCodeGroup = _repository.LookupCodeGroup.GetLookupCodeGroup(id, trackChanges: false);
            if (lookupCodeGroup == null)
            {
                _logger.LogInfo($"LookupCodeGroup con id {id} no existe en la base de datos.");
                return NotFound();
            }

            _repository.LookupCodeGroup.DeleteLookupCodeGroup(lookupCodeGroup);
            _repository.Save();

            return NoContent();
        }
    }
}
