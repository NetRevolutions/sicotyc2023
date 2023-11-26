using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace Sicotyc.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/lookupCodeGroup/{lookupCodeGroupId}/lookupCodes")]
    [ApiController]
    public class LookupCodeController : ControllerBase
    {
        private readonly IServiceManager _service;
        public LookupCodeController(IServiceManager service) => _service = service;

        [HttpGet]
        public IActionResult GetLookupCodesForLookupCodeGroup(Guid lookupCodeGroupId)
        {
            var lookupCodes = _service.LookupCodeService.GetLookupCodes(lookupCodeGroupId, trackChanges: false);

            return Ok(lookupCodes);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetLookupCodeForLookupCodeGroup(Guid lookupCodeGroupId, Guid id)
        {
            var lookupCode = _service.LookupCodeService.GetLookupCode(lookupCodeGroupId, id, trackChanges: false);

            return Ok(lookupCode);
        }
    }
}
