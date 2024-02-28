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
        private readonly IAuthenticationManager _authManager;

        public UploadController(IWebHostEnvironment hostingEnvironment, IUploadFileService uploadFileService, ILoggerManager logger, IMapper mapper, IAuthenticationManager authManager)
        {
            _hostingEnvironment = hostingEnvironment;
            _uploadFileService = uploadFileService;
            _logger = logger;
            _mapper = mapper;
            _authManager = authManager;

        }

        [HttpPut("{type}/{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UploadFile(string type, Guid id)
        {
            string[] validFolders = Enum.GetNames(typeof(FolderFileUploadEnum));
            // Acceder al encabezado "x-token" desde HttpContext
            if (HttpContext.Request.Headers.TryGetValue("x-token", out var tokenHeaderValue))
            {
                // Implementamos validacion del token
                var resultValidateToken = _authManager.ValidateToken(tokenHeaderValue).Result;
                if (!resultValidateToken.Success) {
                    return Unauthorized(resultValidateToken.Message);
                }
                
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
                    _logger.LogError($"Hubo un error al tratar de subir la imagen de tipo {type}, aca el detalle: {ex.Message}");
                    return BadRequest("Hubo un error al tratar de subir la imagen");
                }                
            }
            else {
                return BadRequest("No existe token para realizar esta accion");
            }
            
        }

        [HttpGet("{type}/{imgName}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> GetImage(string type, string imgName)
        {
            try
            {
                // Obtener la ruta fisica del directorio raiz del proyecto
                string rootPath = _hostingEnvironment.ContentRootPath;

                // Construir la ruta fisica completa
                var fullPath = Path.Combine(rootPath, "Uploads", type!, imgName);

                // Verificamos si el archivo existe
                if (!System.IO.File.Exists(fullPath))
                {
                    // Retornamos imagen por defecto
                    imgName = "imagen-no-disponible.jpg";
                    fullPath = Path.Combine(rootPath, "Uploads", imgName);

                    // return NotFound();  // Devolver 404 si la imagen no existe
                }

                var nameSplit = imgName.Split('.');
                var fileExtension = nameSplit[nameSplit.Length - 1];

                // Leer el archivo de imagen como un arreglo de bytes
                byte[] bytesImage = await System.IO.File.ReadAllBytesAsync(fullPath);

                // Devolver la imagen como un archivo
                //return File(bytesImage, "image/" + fileExtension); // Puedes ajustar el tipo de contenido según el tipo de imagen
                return File(bytesImage, "image/jpeg"); // Puedes ajustar el tipo de contenido según el tipo de imagen
                //return PhysicalFile(fullPath, "image/jpeg");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener la imagen: {ex.Message}");
                return BadRequest("Hubo un error al tratar de obtener la imagen");
            }
            
        }
    }
}
