using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Sicotyc.ActionFilters;

namespace Sicotyc.Controllers
{
    [Route("api/email")]
    [ApiController]
    public class EmailController : Controller
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _respository;
        private readonly IEmailService _emailService;

        public EmailController(ILoggerManager logger, IMapper mapper, IRepositoryManager respository, IEmailService emailService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _respository = respository ?? throw new ArgumentNullException(nameof(respository));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        [HttpPost("sendMail")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> SendEmail([FromBody] EmailMetadata emailMetadata)
        {
            var result = await _emailService.SendMailAsync(emailMetadata);

            if (result)
                return Ok("Correo enviado");
            else
                return BadRequest("Se produjo un error al enviar el correo.");
        }

    }
}
