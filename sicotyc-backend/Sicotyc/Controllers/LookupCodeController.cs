using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Enum;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Contracts;
using Sicotyc.ActionFilters;
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
        private readonly IAuthenticationManager _authManager;
        public LookupCodeController(IServiceManager service, IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IAuthenticationManager authManager) 
        { 
            _service = service;
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _authManager = authManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetLookupCodesForLookupCodeGroup(Guid lookupCodeGroupId, [FromQuery] LookupCodeParameters lookupCodeParameters)
        {
            var lookupCodeGroup = await _repository.LookupCodeGroup.GetLookupCodeGroupAsync(lookupCodeGroupId, trackChanges: false);
            if (lookupCodeGroup == null)
            {
                _logger.LogInfo($"El LookupCode con id: {lookupCodeGroupId} no existe en la base de datos.");
                return NotFound();
            }

            var lookupCodesFromDb = await _repository.LookupCode.GetLookupCodesAsync(lookupCodeGroupId, lookupCodeParameters, trackChanges: false);


            var lookupCodesDto = _mapper.Map<IEnumerable<LookupCodeDto>>(lookupCodesFromDb);
            return Ok(new
            {
                data = lookupCodesDto,
                pagination = lookupCodesFromDb.MetaData
            });
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetLookupCodesForLookupCodeGroupAll(Guid lookupCodeGroupId)
        {
            var lookupCodeGroup = await _repository.LookupCodeGroup.GetLookupCodeGroupAsync(lookupCodeGroupId, trackChanges: false);
            if (lookupCodeGroup == null)
            {
                _logger.LogInfo($"El LookupCode con id: {lookupCodeGroupId} no existe en la base de datos.");
                return NotFound();
            }

            var lookupCodesFromDb = await _repository.LookupCode.GetLookupCodesAsync(lookupCodeGroupId, trackChanges: false);
            var lookupCodesDto = _mapper.Map<IEnumerable<LookupCodeDto>>(lookupCodesFromDb);

            return Ok(new { data = lookupCodesDto });
        }

        [HttpGet("{id:guid}", Name ="GetLookupCodeForLookupCodeGroup")]
        public async Task<IActionResult> GetLookupCodeForLookupCodeGroup(Guid lookupCodeGroupId, Guid id)
        {
            var lookupCode = await _repository.LookupCode.GetLookupCodeAsync(lookupCodeGroupId, id, trackChanges: false);  
            
            if (lookupCode == null) 
            {
                _logger.LogError($"El LookupCode con id: {id} no existe en la base de datos.");
                return NotFound();
            }
            else
            {
                var lookupCodeDto = _mapper.Map<LookupCodeDto>(lookupCode);
                return Ok(lookupCodeDto);
            }
        }

        [HttpGet("LookupCodesByGroupName/{lookupCodeGroupName}")]
        public async Task<IActionResult> GetLookupCodeForLookupCodeGroupName(string lookupCodeGroupName)
        { 
            var lookupCodeGroupsAll = await _repository.LookupCodeGroup.GetAllLookupCodeGroupsAsync(trackChanges: false);
            var lookupCodeGroup = lookupCodeGroupsAll.Where(lcg => lcg.Name.Trim().ToLower() == lookupCodeGroupName.Trim().ToLower()).FirstOrDefault();
            if (lookupCodeGroup == null)
            {
                _logger.LogError("Parametro lookupCodeGroup es nulo o no existe");
                return BadRequest("Parametro lookupCodeGroup es nulo o no existe");
            }
            else
            {
                var lookupCodesFromDb = await _repository.LookupCode.GetLookupCodesAsync(lookupCodeGroup.Id, trackChanges: false);
                if (!lookupCodesFromDb.Any())
                {
                    _logger.LogError($"No existen LookupCodes para el LookupCodeGroup { lookupCodeGroupName }");
                    return NotFound();
                }
                else 
                {
                    var lookupCodesDto = _mapper.Map<IEnumerable<LookupCodeDto>>(lookupCodesFromDb);
                    return Ok(new { data = lookupCodesDto });
                }
            }
        }

        [HttpGet("collection({ids})", Name = "LookupCodeCollection")]
        public async Task<IActionResult> GetLookupCodeCollection(
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids) { 
            if (ids == null)
            {
                _logger.LogError("Parametro ids es nulo");
                return BadRequest("Parametro ids es nulo");
            }

            var lookupCodeEntities = await _repository.LookupCode.GetByIdsAsync(ids, trackChanges: false);
            if (ids.Count() != lookupCodeEntities.Count())
            {
                _logger.LogError("Algunos de los Ids de la coleccion no son validos");
                return NotFound();
            }

            var lookupCodesToReturn = _mapper.Map<IEnumerable<LookupCodeDto>>(lookupCodeEntities);
            return Ok(lookupCodesToReturn);
        }

        [HttpPost("collection")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidationTokenFilter))]
        public async Task<IActionResult> CreateLookupCodeCollectionForLookupCodeGroup(Guid lookupCodeGroupId, [FromBody] LookupCodeCollectionForCreationDto lookupCodes) 
        {
            try
            {
                var lastLookupCodeOrder = await _repository.LookupCode.GetLastLookupCodeOrderAsync(lookupCodeGroupId);
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

                    await _repository.SaveAsync();

                    var lookupCodeCollectionToReturn = _mapper.Map<IEnumerable<LookupCodeDto>>(lookupCodeCollectionEntity);
                    var ids = string.Join(",", lookupCodeCollectionToReturn.Select(s => s.Id));

                    return CreatedAtRoute("LookupCodeCollection", new { ids, lookupCodeGroupId }, lookupCodeCollectionToReturn);
                }
                else
                {
                    _logger.LogWarn("El objeto lookupCodeCollectionEntity no tiene elementos para insertar");
                    return BadRequest("El objeto lookupCodeCollectionEntity no tiene elementos para insertar");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Hubo un error al tratar de crear el LookupCodeCollection, aca el detalle: {ex.Message}");
                return BadRequest("Hubo un error en la ejecucion del metodo");
            }
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidationTokenFilter))]
        public async Task<IActionResult> CreateLookupCodeForLookupCodeGroup(Guid lookupCodeGroupId, [FromBody] LookupCodeForCreationDto lookupCode)
        {
            try
            {
                var lookupCodeGroup = await _repository.LookupCodeGroup.GetLookupCodeGroupAsync(lookupCodeGroupId, trackChanges: false);
                if (lookupCodeGroup == null)
                {
                    _logger.LogInfo($"LookupCodeGroup con id: {lookupCodeGroupId} no existe en la base de datos.");
                    return NotFound();
                }

                if (!lookupCode.LookupCodeOrder.HasValue)
                {
                    // Obtenemos el ultimo lookupOrder creado                
                    int lastLookupCodeOrder = await _repository.LookupCode.GetLastLookupCodeOrderAsync(lookupCodeGroupId);

                    lastLookupCodeOrder++;

                    lookupCode.LookupCodeOrder = lastLookupCodeOrder;
                }

                var lookupCodeEntity = _mapper.Map<LookupCode>(lookupCode);

                _repository.LookupCode.CreateLookupCodeForLookupCodeGroup(lookupCodeGroupId, lookupCodeEntity);
                await _repository.SaveAsync();

                var lookupCodeToReturn = _mapper.Map<LookupCodeDto>(lookupCodeEntity);

                return CreatedAtRoute("GetLookupCodeForLookupCodeGroup", new { lookupCodeGroupId, id = lookupCodeToReturn.Id }, lookupCodeToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Hubo un error al tratar de crear el LookupCode, aca el detalle: {ex.Message}");
                return BadRequest("Hubo un error en la ejecucion del metodo");
            }
        }        

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidationTokenFilter))]
        public async Task<IActionResult> UpdateLookupCodeForLookupCodeGroup(Guid lookupCodeGroupId, Guid id, [FromBody]LookupCodeForUpdateDto lookupCode)
        {
            try
            {
                var lookupCodeGroup = await _repository.LookupCodeGroup.GetLookupCodeGroupAsync(lookupCodeGroupId, trackChanges: false);
                if (lookupCodeGroup == null)
                {
                    _logger.LogInfo($"LookupCodeGroup con id: {lookupCodeGroupId} no existe en la base de datos.");
                    return NotFound();
                }

                var lookupCodeEntity = await _repository.LookupCode.GetLookupCodeAsync(lookupCodeGroupId, id, trackChanges: true);
                if (lookupCodeEntity == null)
                {
                    _logger.LogInfo($"LookupCode con id: {id} no existe en la base de datos.");
                    return NotFound();
                }

                _mapper.Map(lookupCode, lookupCodeEntity);

                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Hubo un error al tratar de actualizar el LookupCode, aca el detalle: {ex.Message}");
                return BadRequest("Hubo un error en la ejecucion del metodo");
            }

        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidationTokenFilter))]
        public async Task<IActionResult> DeleteLookupCodeForLookupCodeGroup(Guid lookupCodeGroupId, Guid id)
        {
            try
            {
                var lookupCodeGroup = await _repository.LookupCodeGroup.GetLookupCodeGroupAsync(lookupCodeGroupId, trackChanges: false);
                if (lookupCodeGroup == null)
                {
                    _logger.LogInfo($"LookupCodeGroup con id: {lookupCodeGroupId} no existe en la base de datos");
                    return NotFound();
                }

                var lookupCodeForLookupCodeGroup = await _repository.LookupCode.GetLookupCodeAsync(lookupCodeGroupId, id, trackChanges: false);
                if (lookupCodeForLookupCodeGroup == null)
                {
                    _logger.LogInfo($"LookupCode con id: {id} no existe en la base de datos.");
                    return NotFound();
                }

                _repository.LookupCode.DeleteLookupCode(lookupCodeForLookupCodeGroup);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Hubo un error al tratar de eliminar el LookupCode, aca el detalle: {ex.Message}");
                return BadRequest("Hubo un error en la ejecucion del metodo");
            }
        }
    }
}
