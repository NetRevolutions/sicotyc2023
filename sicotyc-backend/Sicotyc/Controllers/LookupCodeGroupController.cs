using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Sicotyc.ActionFilters;
using Sicotyc.ModelBinders;
using System.Diagnostics.Eventing.Reader;

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
        private readonly IAuthenticationManager _authManager;
        public LookupCodeGroupController(IServiceManager service, IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IAuthenticationManager authManager)
        {
            _service = service;
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _authManager = authManager;
        }

        //[HttpGet]
        //[HttpGet(Name = "GetLookupCodeGroups"), Authorize] // Con esto implementamos autorizacion a los endpoints que deseamos
        //[HttpGet(Name = "GetLookupCodeGroups"), Authorize(Roles = "Manager,Administrator")] // Con esto indicamos que solo un determinado rol tiene acceso
        [HttpGet("GetLookupCodeGroups")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> GetLookupCodeGroups([FromQuery] LookupCodeGroupParameters lookupCodeGroupParameters) // Mejorar para cambiarlo por ActionResult
        {
            try
            {
                var lookupCodeGroupsDb = await _repository.LookupCodeGroup.GetAllLookupCodeGroupsAsync(lookupCodeGroupParameters, trackChanges: false);

                var lookupCodeGroupsDto = _mapper.Map<IEnumerable<LookupCodeGroupDto>>(lookupCodeGroupsDb);
                return Ok(new
                {
                    data = lookupCodeGroupsDto,
                    pagination = lookupCodeGroupsDb.MetaData
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Hubo un error al tratar de realizar la busqueda de lookupCodeGroups, aca el detalle: {ex.Message}");
                return BadRequest("Hubo un error al tratar de realizar la busqueda de lookupCodeGroups");
            }

        }

        [HttpGet("GetLookupCodeGroups/All")] //without pagination
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> GetLookupCodeGroupsAll()
        {
            try
            {
                var lookupCodeGroupsDb = await _repository.LookupCodeGroup.GetAllLookupCodeGroupsAsync(trackChanges: false);
                var lookupCodeGroupsDto = _mapper.Map<IEnumerable<LookupCodeGroupDto>>(lookupCodeGroupsDb);

                return Ok(lookupCodeGroupsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Hubo un error al tratar de realizar la busqueda de lookupCodeGroups (All), aca el detalle: {ex.Message}");
                return BadRequest("Hubo un error al tratar de realizar la busqueda de lookupCodeGroups (All)");
            }
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

        [HttpGet("collection({ids})", Name = "LookupCodeGroupCollection")]
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

        [HttpGet("existsLookupCodeGroup/{name}")]
        public async Task<IActionResult> ExistsLookupCodeGroup(string name) { 
            var existsLCG = await _repository.LookupCodeGroup.ExistsLookupCodeGroupAsync(name, trackChanges: false);
            return Ok(existsLCG);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateLookupCodeGroup([FromBody] LookupCodeGroupForCreationDto lookupCodeGroup) 
        {
            // Acceder al encabezado "x-token" desde HttpContext
            if (HttpContext.Request.Headers.TryGetValue("x-token", out var tokenHeaderValue))
            {
                // Implementamos validacion del token
                var resultValidateToken = _authManager.ValidateToken(tokenHeaderValue).Result;
                if (!resultValidateToken.Success)
                {
                    return Unauthorized(resultValidateToken.Message);
                }
                try
                {
                    var lookupCodeGroupEntity = _mapper.Map<LookupCodeGroup>(lookupCodeGroup);
                    var claims = await _repository.AuthenticationManager.GetClaimsAsync(tokenHeaderValue.ToString());

                    if (lookupCodeGroupEntity.LookupCodes?.Count > 0)
                    {
                        foreach (var item in lookupCodeGroupEntity.LookupCodes)
                        {
                            item.LookupCodeGroupId = lookupCodeGroupEntity.Id;
                            item.CreatedBy = claims.Count() > 0 ? claims.Find(x => x.Type == "Id").Value : "system";
                        }
                    }

                    _repository.LookupCodeGroup.CreateLookupCodeGroup(lookupCodeGroupEntity);
                    await _repository.SaveAsync();

                    var lookupCodeGroupToReturn = _mapper.Map<LookupCodeGroupDto>(lookupCodeGroupEntity);

                    return CreatedAtRoute("LookupCodeGroupById", new { id = lookupCodeGroupToReturn.Id }, lookupCodeGroupToReturn);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Hubo un error al tratar de crear el LookupCodeGroup aca el detalle: {ex.Message}");
                    return BadRequest("Hubo un error en la ejecucion del metodo");
                }

            }
            else
            {
                return BadRequest("No hay token en la peticion");
            }            
        }

        [HttpPost("collection")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateLookupCodeGroupCollection([FromBody] IEnumerable<LookupCodeGroupForCreationDto> lookupCodeGroupCollection) 
        {
            // Acceder al encabezado "x-token" desde HttpContext
            if (HttpContext.Request.Headers.TryGetValue("x-token", out var tokenHeaderValue))
            {
                // Implementamos validacion del token
                var resultValidateToken = _authManager.ValidateToken(tokenHeaderValue).Result;
                if (!resultValidateToken.Success)
                {
                    return Unauthorized(resultValidateToken.Message);
                }

                try
                {
                    var lookupCodeGroupEntities = _mapper.Map<IEnumerable<LookupCodeGroup>>(lookupCodeGroupCollection);

                    var claims = await _repository.AuthenticationManager.GetClaimsAsync(tokenHeaderValue.ToString());
                    foreach (var lookupCodeGroup in lookupCodeGroupEntities)
                    {
                        lookupCodeGroup.CreatedBy = claims.Count() > 0 ? claims.Find(x => x.Type == "Id").Value : "system";
                        _repository.LookupCodeGroup.CreateLookupCodeGroup(lookupCodeGroup);
                    }

                    await _repository.SaveAsync();

                    var lookupCodeGroupCollectionToReturn = _mapper.Map<IEnumerable<LookupCodeGroupDto>>(lookupCodeGroupEntities);
                    var ids = string.Join(",", lookupCodeGroupCollectionToReturn.Select(c => c.Id));

                    return CreatedAtRoute("LookupCodeGroupCollection", new { ids }, lookupCodeGroupCollectionToReturn);

                }
                catch (Exception ex)
                {
                    _logger.LogError($"Hubo un error al tratar de crear el LookupCodeGroupCollection aca el detalle: {ex.Message}");
                    return BadRequest("Hubo un error en la ejecucion del metodo");
                }
            }
            else
            {
                return BadRequest("No hay token en la peticion");
            }            
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidationTokenFilter))]
        public async Task<IActionResult> UpdateLookupCodeGroup(Guid id, [FromBody] LookupCodeGroupForUpdateDto lookupCodeGroup) 
        {
            try
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
                else
                {
                    _mapper.Map(lookupCodeGroup, lookupCodeGroupEntity);
                }

                await _repository.SaveAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Hubo un error al tratar de actualizar el LookupCodeGroup, aca el detalle: {ex.Message}");
                return BadRequest("Hubo un error en la ejecucion del metodo");
            }
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidationTokenFilter))]
        public async Task<IActionResult> DeleteLookupCodeGroup(Guid id)
        {
            try
            {
                var lookupCodeGroup = await _repository.LookupCodeGroup.GetLookupCodeGroupAsync(id, trackChanges: false);
                if (lookupCodeGroup == null)
                {
                    _logger.LogInfo($"LookupCodeGroup con id {id} no existe en la base de datos.");
                    return NotFound();
                }

                // 1.- Primero verificar si tiene Lookup Codes Asociados
                var lookupCodes = await _repository.LookupCode.GetLookupCodesAsync(id, trackChanges: false);
                if (lookupCodes.Any())
                {
                    // 2.- Borrar los Lookup Codes asociados
                    foreach (var item in lookupCodes)
                    {
                        _repository.LookupCode.DeleteLookupCode(item);
                    }
                    await _repository.SaveAsync();
                }

                // 3.- Borrar el Lookup Code Group
                _repository.LookupCodeGroup.DeleteLookupCodeGroup(lookupCodeGroup);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Hubo un error al tratar de eliminar el LookupCodeGroup, aca el detalle: {ex.Message}");
                return BadRequest("Hubo un error en la ejecucion del metodo");
            }
        }
    }
}
