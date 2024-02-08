using AutoMapper;
using Contracts;
using Entities.Enum;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Sicotyc.ActionFilters;

namespace Sicotyc.Controllers
{
    [Route("api/upload")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IUploadFileService _uploadFileService;

        public UploadController(IWebHostEnvironment hostingEnvironment, IUploadFileService uploadFileService, ILoggerManager logger, IMapper mapper)
        {
            _hostingEnvironment = hostingEnvironment;
            _uploadFileService = uploadFileService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPut("{type}/{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UploadFile(string type, Guid id)
        {
            string[] validFolders = Enum.GetNames(typeof(FolderFileUploadEnum));
            // Acceder al encabezado "x-token" desde HttpContext
            if (HttpContext.Request.Headers.TryGetValue("x-token", out var tokenHeaderValue))
            {
                if (!validFolders.Contains(type?.ToUpper()))
                {
                    return BadRequest($"No existe una capeta para este tipo de archivo ({type}) a subir");
                }

                try
                {
                    var fileToUpload = Request.Form.Files[0];
                    if (fileToUpload == null)
                    {
                        return BadRequest("No hay ningun archivo seleccionado");
                    }

                    var nameSplit = fileToUpload.FileName.Split('.');
                    var fileExtension = nameSplit[nameSplit.Length - 1];

                    string[] validExtensionFiles = Enum.GetNames(typeof(ImageFileExtensionEnum));
                    if (!validExtensionFiles.Contains(fileExtension?.ToUpper()))
                    {
                        return BadRequest($"No es una extension permitida");
                    }

                    // Generar el nombre del archivo
                    var fileName = Guid.NewGuid() + "." + fileExtension!.ToString();

                    // Obtener la ruta fisica del directorio raiz del proyecto
                    string rootPath = _hostingEnvironment.ContentRootPath;

                    // Path para guardar la imagen
                    var filePath = Path.Combine(rootPath, "Uploads", type!, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await fileToUpload.CopyToAsync(stream);

                        // Actualizar base de datos
                        await _uploadFileService.UpdateImageAsync(type!, id, rootPath, fileName);

                        // Tenemos que retornar el nombre del archivo creado
                        return Ok(new { FileName = fileName });                        
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Hubo un error al tratar de subir la imagen de tipo {type}, aca el detalle: ${ex.Message}");
                    return BadRequest("Hubo un error al tratar de subir la imagen");
                }                
            }
            else {
                return BadRequest("No existe token para realizar esta accion");
            }
            
        }
    }
}
