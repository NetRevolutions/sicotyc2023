using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using Tesseract;

namespace Sicotyc.Controllers
{
    [Route("api/sunat")]
    [ApiController]
    public class SunatController : Controller
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public SunatController(IHttpClientFactory httpClientFactory, ILoggerManager logger, IMapper mapper, IRepositoryManager repository, IConfiguration configuration)
        {

            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
            _configuration = configuration;
        }

        [HttpGet("{ruc}")]
        public async Task<IActionResult> ConsultarRuc(string ruc) {
            int tipoRespuesta = 2;
            string mensajeRespuesta = "";
            string tokenSunat = "";
            var sunatResponse = await GetSunatTokenResponseAsync();
            if (sunatResponse != null)
            {
                tokenSunat = sunatResponse.access_token;
            }

            if (string.IsNullOrEmpty(ruc))
            {
                return BadRequest("El ruc es requerido");
            }

            CookieContainer cookies = new CookieContainer();
            HttpClientHandler controladorMensaje = new HttpClientHandler();
            controladorMensaje.CookieContainer = cookies;
            controladorMensaje.UseCookies = true;
            using (HttpClient cliente = new HttpClient(controladorMensaje))
            {
                cliente.DefaultRequestHeaders.Add("Host", "e-consultaruc.sunat.gob.pe");
                cliente.DefaultRequestHeaders.Add("User-Agent",
                    "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.150 Safari/537.36");
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 |
                                                       SecurityProtocolType.Tls12;
                //string[] arrResultadoValidarImagenCaptcha = await ValidateCatchaImage(cliente, 5);
                //if (arrResultadoValidarImagenCaptcha[0] == "1")
                if (true)
                {
                    var lClaveValor = new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("accion", "consPorRuc"),
                        new KeyValuePair<string, string>("nroRuc", ruc),
                        new KeyValuePair<string, string>("token", tokenSunat),
                        new KeyValuePair<string, string>("contexto", "ti-it"),
                        new KeyValuePair<string, string>("modo", "1"),
                        new KeyValuePair<string, string>("rbtnTipo", "1"),
                        new KeyValuePair<string, string>("search1", ruc),
                        new KeyValuePair<string, string>("tipdoc", "1"),
                        //new KeyValuePair<string, string>("codigo", arrResultadoValidarImagenCaptcha[1])
                    };
                    FormUrlEncodedContent contenido = new FormUrlEncodedContent(lClaveValor);
                    //string url = "https://e-consultaruc.sunat.gob.pe/cl-ti-itmrconsruc/jcrS03Alias";
                    string url = "https://e-consultaruc.sunat.gob.pe/cl-ti-itmrconsruc/jcrS00Alias";
                    using (HttpResponseMessage resultadoConsultaDatos = await cliente.PostAsync(url, contenido))
                    {
                        if (resultadoConsultaDatos.IsSuccessStatusCode)
                        {
                            string contenidoHTML = await resultadoConsultaDatos.Content.ReadAsStringAsync();
                            contenidoHTML = WebUtility.HtmlDecode(contenidoHTML);
                            DatosRUC oDatosRUC = ObtenerDatos(contenidoHTML);
                            if (oDatosRUC.TipoRespuesta == 1)
                            {
                                return Ok(new
                                {
                                    datosRuc = oDatosRUC,
                                    tipoRespuesta = 1,
                                    mensajeRespuesta = string.Format("Se realizó exitosamente la consulta del número de RUC {0}", ruc)
                                });
                            }
                            else
                            {
                                return BadRequest(new
                                {
                                    tipoRespuesta = oDatosRUC.TipoRespuesta,
                                    mensajeRespuesta = string.Format(
                                    "No se pudo realizar la consulta del número de RUC {0}.\r\nDetalle: {1}", ruc, oDatosRUC.MensajeRespuesta)
                                });
                            }
                        }
                        else 
                        {
                            mensajeRespuesta = await resultadoConsultaDatos.Content.ReadAsStringAsync();
                            mensajeRespuesta = string.Format("Ocurrió un inconveniente al consultar los datos del RUC {0}.\r\nDetalle:{1}", ruc, mensajeRespuesta);

                            return BadRequest(new { 
                                mensajeRespuesta
                            });
                        }
                        
                    }
                }
                else
                {
                    tipoRespuesta = 2;//Convert.ToInt16(arrResultadoValidarImagenCaptcha[0]);
                    mensajeRespuesta = "Hubo un error";//arrResultadoValidarImagenCaptcha[1];
                }
            }
            if (tipoRespuesta > 1)
            {
                return BadRequest(new
                {
                    tipoRespuesta,
                    mensajeRespuesta
                });
            }
            else {
                return Ok(new {
                    tipoRespuesta = 1,
                    mensajeRespuesta = string.Format("Se realizó exitosamente la consulta del número de RUC {0}", ruc)
                });
            }
        }

        [HttpPost("sunatToken")]
        public async Task<IActionResult> GetSunatToken() {
            try
            {
                
                var sunatResponse = await GetSunatTokenResponseAsync();

                return Ok(sunatResponse);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }            
        }

        private async Task<SunatTokenResponse> GetSunatTokenResponseAsync() {
            var sunatSettings = _configuration.GetSection("SunatSettings");
            var formData = new Dictionary<string, string>
                {
                    { "grant_type", sunatSettings.GetSection("grant_type").Value! },
                    { "scope", sunatSettings.GetSection("scope").Value! },
                    { "client_id", sunatSettings.GetSection("client_id").Value! },
                    { "client_secret", sunatSettings.GetSection("client_secret").Value! }
                };

            var content = new FormUrlEncodedContent(formData);
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.PostAsync($"https://api-seguridad.sunat.gob.pe/v1/clientesextranet/{sunatSettings.GetSection("client_Id").Value}/oauth2/token/", content);
            if (response.IsSuccessStatusCode)
            {
                // Leer el contenido de la respuesta
                var contentStream = await response.Content.ReadAsStreamAsync();
                var sunatResponse = await JsonSerializer.DeserializeAsync<SunatTokenResponse>(contentStream);

                return sunatResponse!;
            }
            else
            {
                throw new Exception("Hubo un error al momento de generar el TokenSunat");
            }
        }

        private async Task<string[]> ValidateCatchaImage(HttpClient client, short totalAttemps = 3)
        {
            int answerType = 2;
            string answerMessage = "Mensaje de inconveniente no especificado";
            try
            {
                string url = "https://e-consultaruc.sunat.gob.pe/cl-ti-itmrconsruc/captcha?accion=image";
                byte[] arrContent;

                float confidentPercentage = 0;
                int cAttemps = 0;
                while (confidentPercentage < 0.51 && cAttemps < totalAttemps + 1)
                {
                    cAttemps++;
                    using (HttpResponseMessage answerImageConsultant = await client.GetAsync(url))
                    {
                        if (answerImageConsultant.IsSuccessStatusCode) 
                        {
                            using (Stream stmContent = await answerImageConsultant.Content.ReadAsStreamAsync())
                            {
                                arrContent = new byte[stmContent.Length];
                                await stmContent.ReadAsync(arrContent, 0, arrContent.Length);
                            }
                            if (arrContent.Length > 0)
                            {
                                using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
                                {
                                    using (var img = Pix.LoadFromMemory(arrContent))
                                    {
                                        using (var page = engine.Process(img))
                                        { 
                                            answerMessage = page.GetText();
                                            confidentPercentage = page.GetMeanConfidence();
                                            if (confidentPercentage > 0.50) 
                                            {
                                                answerType = 1;
                                                answerMessage = answerMessage.Replace("\n", "").Trim().ToUpper();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (answerType == 2 && cAttemps == totalAttemps)
                    answerMessage = "No se pudo validar la imagen mayor del 50% de confianza en el proceso";
            }
            catch (Exception ex)
            {
                answerType = 3;
                answerMessage = ex.Message;
            }

            return new string[2] { answerType.ToString(), answerMessage};
        }

        private string ExtraerContenidoEntreTagString(string cadena, int posicion, string nombreInicio, string nombreFin, StringComparison reglaComparacion = StringComparison.OrdinalIgnoreCase)
        {
            string respuesta = "";
            int posicionInicio = cadena.IndexOf(nombreInicio, posicion, reglaComparacion);
            if (posicionInicio > -1)
            {
                posicionInicio += nombreInicio.Length;
                int posFin = cadena.IndexOf(nombreFin, posicionInicio, reglaComparacion);
                if (posFin > -1)
                    respuesta = cadena.Substring(posicionInicio, posFin - posicionInicio);
            }

            return respuesta;
        }
        private string[] ExtraerContenidoEntreTag(string cadena, int posicion, string nombreInicio, string nombreFin, StringComparison reglaComparacion = StringComparison.OrdinalIgnoreCase)
        {
            string[] arrRespuesta = null;
            int posicionInicio = cadena.IndexOf(nombreInicio, posicion, reglaComparacion);
            if (posicionInicio > -1)
            {
                posicionInicio += nombreInicio.Length;
                int posFin = cadena.IndexOf(nombreFin, posicionInicio, reglaComparacion);
                if (posFin > -1)
                {
                    posicion = posFin + nombreFin.Length;
                    arrRespuesta = new string[2];
                    arrRespuesta[0] = posicion.ToString();
                    arrRespuesta[1] = cadena.Substring(posicionInicio, posFin - posicionInicio);
                }
            }

            return arrRespuesta;
        }

        private DatosRUC ObtenerDatos(string contenidoHTML)
        {
            DatosRUC oDatosRUC = new DatosRUC();
            string nombreInicio = "<HEAD><TITLE>";
            string nombreFin = "</TITLE></HEAD>";
            string contenidoBusqueda = ExtraerContenidoEntreTagString(contenidoHTML, 0, nombreInicio, nombreFin);
            if (contenidoBusqueda == ".:: Pagina de Mensajes ::.")
            {
                nombreInicio = "<p class=\"error\">";
                nombreFin = "</p>";
                oDatosRUC.TipoRespuesta = 2;
                oDatosRUC.MensajeRespuesta = ExtraerContenidoEntreTagString(contenidoHTML, 0, nombreInicio, nombreFin);
            }
            else if (contenidoBusqueda == ".:: Pagina de Error ::.")
            {
                nombreInicio = "<p class=\"error\">";
                nombreFin = "</p>";
                oDatosRUC.TipoRespuesta = 3;
                oDatosRUC.MensajeRespuesta = ExtraerContenidoEntreTagString(contenidoHTML, 0, nombreInicio, nombreFin);
            }
            else
            {
                oDatosRUC.TipoRespuesta = 2;
                nombreInicio = "<div class=\"list-group\">";
                nombreFin = "<div class=\"panel-footer text-center\">";
                contenidoBusqueda = ExtraerContenidoEntreTagString(contenidoHTML, 0, nombreInicio, nombreFin);
                if (contenidoBusqueda == "")
                {
                    nombreInicio = "<strong>";
                    nombreFin = "</strong>";
                    oDatosRUC.MensajeRespuesta = ExtraerContenidoEntreTagString(contenidoHTML, 0, nombreInicio, nombreFin);
                    if (oDatosRUC.MensajeRespuesta == "")
                        oDatosRUC.MensajeRespuesta = "No se encuentra las cabeceras principales del contenido HTML";
                }
                else
                {
                    contenidoHTML = contenidoBusqueda;
                    oDatosRUC.MensajeRespuesta = "Mensaje del inconveniente no especificado";
                    nombreInicio = "<h4 class=\"list-group-item-heading\">";
                    nombreFin = "</h4>";
                    int resultadoBusqueda = contenidoHTML.IndexOf(nombreInicio, 0, StringComparison.OrdinalIgnoreCase);
                    if (resultadoBusqueda > -1)
                    {
                        resultadoBusqueda += nombreInicio.Length;
                        string[] arrResultado = ExtraerContenidoEntreTag(contenidoHTML, resultadoBusqueda,
                            nombreInicio, nombreFin);
                        if (arrResultado != null)
                        {
                            oDatosRUC.RUC = arrResultado[1];

                            // Tipo Contribuyente
                            nombreInicio = "<p class=\"list-group-item-text\">";
                            nombreFin = "</p>";
                            arrResultado = ExtraerContenidoEntreTag(contenidoHTML, Convert.ToInt32(arrResultado[0]),
                                nombreInicio, nombreFin);
                            if (arrResultado != null)
                            {
                                oDatosRUC.TipoContribuyente = arrResultado[1];

                                // Nombre Comercial
                                arrResultado = ExtraerContenidoEntreTag(contenidoHTML, Convert.ToInt32(arrResultado[0]),
                                    nombreInicio, nombreFin);
                                if (arrResultado != null)
                                {
                                    oDatosRUC.NombreComercial = arrResultado[1].Replace("\r\n", "").Replace("\t", "").Trim();

                                    // Fecha de Inscripción
                                    arrResultado = ExtraerContenidoEntreTag(contenidoHTML, Convert.ToInt32(arrResultado[0]),
                                        nombreInicio, nombreFin);
                                    if (arrResultado != null)
                                    {
                                        oDatosRUC.FechaInscripcion = arrResultado[1];

                                        // Estado del Contribuyente
                                        arrResultado = ExtraerContenidoEntreTag(contenidoHTML, Convert.ToInt32(arrResultado[0]),
                                            nombreInicio, nombreFin);
                                        if (arrResultado != null)
                                        {
                                            oDatosRUC.EstadoContribuyente = arrResultado[1].Trim();

                                            // Condición del Contribuyente
                                            arrResultado = ExtraerContenidoEntreTag(contenidoHTML, Convert.ToInt32(arrResultado[0]),
                                                nombreInicio, nombreFin);
                                            if (arrResultado != null)
                                            {
                                                oDatosRUC.CondicionContribuyente = arrResultado[1].Trim();

                                                // Domicilio Fiscal
                                                arrResultado = ExtraerContenidoEntreTag(contenidoHTML, Convert.ToInt32(arrResultado[0]),
                                                    nombreInicio, nombreFin);
                                                if (arrResultado != null)
                                                {
                                                    oDatosRUC.DomicilioFiscal = arrResultado[1].Trim();

                                                    // Actividad(es) Económica(s)
                                                    nombreInicio = "<tbody>";
                                                    nombreFin = "</tbody>";
                                                    arrResultado = ExtraerContenidoEntreTag(contenidoHTML, Convert.ToInt32(arrResultado[0]),
                                                        nombreInicio, nombreFin);
                                                    if (arrResultado != null)
                                                    {
                                                        oDatosRUC.ActividadesEconomicas = arrResultado[1].Replace("\r\n", "").Replace("\t", "").Trim();

                                                        // Comprobantes de Pago c/aut. de impresión (F. 806 u 816)
                                                        arrResultado = ExtraerContenidoEntreTag(contenidoHTML, Convert.ToInt32(arrResultado[0]),
                                                            nombreInicio, nombreFin);
                                                        if (arrResultado != null)
                                                        {
                                                            oDatosRUC.ComprobantesPago = arrResultado[1].Replace("\r\n", "").Replace("\t", "").Trim();

                                                            // Sistema de Emisión Electrónica
                                                            arrResultado = ExtraerContenidoEntreTag(contenidoHTML, Convert.ToInt32(arrResultado[0]),
                                                                nombreInicio, nombreFin);
                                                            if (arrResultado != null)
                                                            {
                                                                oDatosRUC.SistemaEmisionComprobante = arrResultado[1].Replace("\r\n", "").Replace("\t", "").Trim();

                                                                // Afiliado al PLE desde
                                                                nombreInicio = "<p class=\"list-group-item-text\">";
                                                                nombreFin = "</p>";
                                                                arrResultado = ExtraerContenidoEntreTag(contenidoHTML, Convert.ToInt32(arrResultado[0]),
                                                                    nombreInicio, nombreFin);
                                                                if (arrResultado != null)
                                                                {
                                                                    oDatosRUC.AfiliadoPLEDesde = arrResultado[1];

                                                                    // Padrones 
                                                                    nombreInicio = "<tbody>";
                                                                    nombreFin = "</tbody>";
                                                                    arrResultado = ExtraerContenidoEntreTag(contenidoHTML, Convert.ToInt32(arrResultado[0]),
                                                                        nombreInicio, nombreFin);
                                                                    if (arrResultado != null)
                                                                    {
                                                                        oDatosRUC.Padrones = arrResultado[1].Replace("\r\n", "").Replace("\t", "").Trim();

                                                                        oDatosRUC.TipoRespuesta = 1;
                                                                        oDatosRUC.MensajeRespuesta = "Ok";
                                                                    }
                                                                }
                                                            }

                                                        }

                                                    }

                                                }

                                            }

                                        }

                                    }
                                }
                            }
                        }
                    }
                }
            }

            return oDatosRUC;
        }

    }
}
