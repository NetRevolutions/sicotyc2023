using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Service.Contracts;
using Sicotyc.ModelBinders;

namespace Sicotyc.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/lookupCodeGroups/{lookupCodeGroupId}/lookupCodes")]
    [ApiController]
    public class LookupCodeController : ControllerBase
    {
        private readonly IServiceManager _service;
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public LookupCodeController(IServiceManager service, IRepositoryManager repository, ILoggerManager logger, IMapper mapper) 
        { 
            _service = service;
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetLookupCodesForLookupCodeGroup(Guid lookupCodeGroupId)
        {
            var lookupCodes = _service.LookupCodeService.GetLookupCodes(lookupCodeGroupId, trackChanges: false);

            return Ok(lookupCodes);
        }

        [HttpGet("{id:guid}", Name ="GetLookupCodeForLookupCodeGroup")]
        public IActionResult GetLookupCodeForLookupCodeGroup(Guid lookupCodeGroupId, Guid id)
        {
            var lookupCode = _service.LookupCodeService.GetLookupCode(lookupCodeGroupId, id, trackChanges: false);

            return Ok(lookupCode);
        }

        [HttpGet("collection/({ids})", Name = "LookupCodeCollection")]
        public IActionResult GetLookupCodeCollection(
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids) { 
            if (ids == null)
            {
                _logger.LogError("Parametro ids es nulo");
                return BadRequest("Parametro ids es nulo");
            }

            var lookupCodeEntities = _repository.LookupCode.GetByIds(ids, trackChanges: false);
            if (ids.Count() != lookupCodeEntities.Count())
            {
                _logger.LogError("Algunos de los Ids de la coleccion no son validos");
                return NotFound();
            }

            var lookupCodesToReturn = _mapper.Map<IEnumerable<LookupCodeDto>>(lookupCodeEntities);
            return Ok(lookupCodesToReturn);
        }

        [HttpPost("collection")]
        public IActionResult CreateLookupCodeCollectionForLookupCodeGroup(Guid lookupCodeGroupId, [FromBody] LookupCodeCollectionForCreationDto lookupCodes) 
        {
            if (lookupCodes == null)
            {
                _logger.LogError("El objeto LookupCodeCollectionForCreationDto enviado por el cliente es nulo");
                return BadRequest("Objeto LookupCodeCollectionForCreationDto es nulo.");
            }

            var lastLookupCodeOrder = _repository.LookupCode.GetLastLookupCodeOrder(lookupCodeGroupId);
            lastLookupCodeOrder++;

            foreach (var item in lookupCodes.LookupCodes)
            {
                item.LookupCodeGroupId = lookupCodeGroupId;
                item.LookupCodeOrder = lastLookupCodeOrder;
                lastLookupCodeOrder++;
            }

            var lookupCodeCollectionEntity = _mapper.Map<IEnumerable<LookupCode>>(lookupCodes.LookupCodes);
            if (lookupCodeCollectionEntity.Count() > 0)
            {
                foreach (var item in lookupCodeCollectionEntity)
                {
                    _repository.LookupCode.CreateLookupCodeForLookupCodeGroup(lookupCodeGroupId, item);
                }

                _repository.Save();

                var lookupCodeCollectionToReturn = _mapper.Map<IEnumerable<LookupCodeDto>>(lookupCodeCollectionEntity);
                var ids = string.Join(",", lookupCodeCollectionToReturn.Select(s => s.Id));

                return CreatedAtRoute("LookupCodeCollection", new { ids,  lookupCodeGroupId }, lookupCodeCollectionToReturn);
            }
            else {
                _logger.LogWarn("El objeto lookupCodeCollectionEntity no tiene elementos para insertar");
                return BadRequest("El objeto lookupCodeCollectionEntity no tiene elementos para insertar");
            }    
        }

        [HttpPost]
        public IActionResult CreateLookupCodeForLookupCodeGroup(Guid lookupCodeGroupId, [FromBody] LookupCodeForCreationDto lookupCode)
        {
            if (lookupCode == null)
            {
                _logger.LogError("El objeto LookupCodeForCreationDto enviado por el cliente es nulo.");
                return BadRequest("Objeto LookupCodeForCreationDto es nulo.");
            }

            var lookupCodeGroup = _repository.LookupCodeGroup.GetLookupCodeGroup(lookupCodeGroupId, trackChanges: false);
            if (lookupCodeGroup == null)
            {
                _logger.LogInfo($"LookupCodeGroup con id: {lookupCodeGroupId} no existe en la base de datos.");
                return NotFound();
            }

            if (!lookupCode.LookupCodeOrder.HasValue)
            {
                // Obtenemos el ultimo lookupOrder creado                
                int lastLookupCodeOrder = _repository.LookupCode.GetLastLookupCodeOrder(lookupCodeGroupId);

                lastLookupCodeOrder++;

                lookupCode.LookupCodeOrder = lastLookupCodeOrder;
            }

            var lookupCodeEntity = _mapper.Map<LookupCode>(lookupCode);

            _repository.LookupCode.CreateLookupCodeForLookupCodeGroup(lookupCodeGroupId, lookupCodeEntity);
            _repository.Save();

            var lookupCodeToReturn = _mapper.Map<LookupCodeDto>(lookupCodeEntity);

            return CreatedAtRoute("GetLookupCodeForLookupCodeGroup", new { lookupCodeGroupId, id = lookupCodeToReturn.Id }, lookupCodeToReturn);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLookupCodeForLookupCodeGroup(Guid lookupCodeGroupId,  Guid id)
        {
            var lookupCodeGroup = _repository.LookupCodeGroup.GetLookupCodeGroup(lookupCodeGroupId, trackChanges: false);
            if ( lookupCodeGroup == null)
            {
                _logger.LogInfo($"LookupCodeGroup con id: {lookupCodeGroupId} no existe en la base de datos");
                return NotFound();
            }

            var lookupCodeForLookupCodeGroup = _repository.LookupCode.GetLookupCode(lookupCodeGroupId, id, trackChanges: false);
            if (lookupCodeForLookupCodeGroup == null)
            {
                _logger.LogInfo($"LookupCode con id: {id} no existe en la base de datos.");
                return NotFound();
            }

            _repository.LookupCode.DeleteLookupCode(lookupCodeForLookupCodeGroup);
            _repository.Save();

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLookupCodeForLookupCodeGroup(Guid lookupCodeGroupId, Guid id, [FromBody]LookupCodeForUpdateDto lookupCode)
        {
            if (lookupCode == null)
            {
                _logger.LogError("El objeto LookupCodeForUpdateDto enviado desde el cliente es nulo.");
                return BadRequest("LookupCodeGroupForUpdateDto es nulo.");
            }

            var lookupCodeGroup = _repository.LookupCodeGroup.GetLookupCodeGroup(lookupCodeGroupId, trackChanges: false);
            if (lookupCodeGroup == null)
            {
                _logger.LogInfo($"LookupCodeGroup con id: {lookupCodeGroupId} no existe en la base de datos.");
                return NotFound();
            }

            var lookupCodeEntity = _repository.LookupCode.GetLookupCode(lookupCodeGroupId, id, trackChanges:true);
            if (lookupCodeEntity == null)
            {
                _logger.LogInfo($"LookupCode con id: {id} no existe en la base de datos.");
                return NotFound();
            }

            _mapper.Map(lookupCode, lookupCodeEntity);
            
            _repository.Save();

            return NoContent();
        }
    }
}
