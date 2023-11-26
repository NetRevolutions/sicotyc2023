using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Service.Contracts;

namespace Sicotyc.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/lookupCodeGroup")]
    [ApiController]
    public class LookupCodeGroupController : ControllerBase
    {
        private readonly IServiceManager _service;
        public LookupCodeGroupController(IServiceManager service) => _service = service;

        [HttpGet]
        public IActionResult GetLookupCodeGroups() // Mejorar para cambiarlo por ActionResult
        {
            try
            {
                var lookupCodeGroups = _service.LookupCodeGroupService.GetAllLookupCodeGroups(trackChanges: false);
                return Ok(lookupCodeGroups);
            }
            catch 
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get() {
            
        //    var result = new string[] { "value1", "value2" };

        //    if (result.Length == 0)
        //    {
        //        return NotFound();
        //    }

        //    return result;
        //}

        [HttpPost]
        public ActionResult Post() {
            return NoContent();
        }

        [HttpPut]
        public ActionResult Put() { 
            return NoContent(); 
        }

        [HttpDelete]
        public ActionResult Delete()
        {
            return NoContent();
        }
    }
}
