namespace Utility
{
    public static class StringUtility
    {
        /// <summary>
        /// Funcion que reemplaza los caracteres extraños por su valor correcto
        /// </summary>
        /// <param name="str"></param>
        /// <returns>string</returns>
        public static string CorregirValoresExtranios(string str)
        {
            str = str.Replace("/'\'s+/g", " ")
                .Replace("&#xFFFD;", "Í")
                .Replace("&#xFFFD;", "Ó")
                .Replace("&#xE9;", "é")
                .Replace("&#xF3;", "ó")
                .Replace("&#xB0;", "°")
                .Replace("&aacute;", "á")
                .Replace("&ntilde;", "ñ")
                .Replace("<!--SC003-2015 Inicio-->", string.Empty)
                .Replace("<!--SC003-2015 Fin-->", string.Empty)
                .Replace("<!-- JRR - 20/09/2010 - Se añade cambio de Igor -->", string.Empty);
            // TODO: Si ubiera mas caracteres a reemplazar simplemente se agrega

            return str;
        }

        /// <summary>
        /// Funcion que retorna la lista de clave/valor de un select en string
        /// </summary>
        /// <param name="str"></param>
        /// <returns>List<string></string></returns>
        public static List<string> ObtenerClaveValorSelect(string str)
        {
            /*
            <select name="select">                
                <option value="00"> Principal        - CIIU 60230 - TRANSPORTE  DE CARGA POR CARRETERA.
            </select>
             */
            List<string> lstClaveValor = new List<string>();

            string[] linesOptions = str.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
            ).Where(x => x.Contains("<option")).ToArray();

            foreach (var item in linesOptions)
            {
                string[] lstTemp = item.Trim().Split('>');
                string tempClave = lstTemp[0].Trim().Replace("<option value=", String.Empty).Replace("\"", String.Empty);
                string tempValor = CorregirValoresExtranios(lstTemp[1]).Trim();
                lstClaveValor.Add(tempClave + " | " + tempValor); // TODO: Revisar
            }

            return lstClaveValor;
        }

        /// <summary>
        /// Funcion que transforma el mensaje de hyperlink a un texto con su mensaje
        /// </summary>
        /// <param name="str"></param>
        /// <param name="textoABuscar"></param>
        /// <returns>string</returns>
        public static string UnirTituloText(string str, string textoABuscar = "NO HABIDO")
        {
            string strResult = String.Empty;
            /*
             <a target="_blank" href="http://www.sunat.gob.pe/orientacion/Nohallados/index.html" 
            title="Deber&aacute; declarar el nuevo domicilio fiscal o confirmar el se&ntilde;alado en el RUC. Para ello, 
            deber&aacute; acercarse a los Centros de Servicios al Contribuyente con los documentos que sustenten el nuevo domicilio.">NO HABIDO</a>
             */

            if (str.Contains(textoABuscar))
            {
                string strTempMsg = textoABuscar + ", ";
                strResult = strTempMsg + CorregirValoresExtranios(str.Split("title=")[1].Split(">")[0]).Replace("\"", String.Empty).Trim();
            }
            else
            {
                strResult = CorregirValoresExtranios(str).Trim();
            }

            return strResult;
        }

        public static string ExtraerContenidoEntreTagString(string cadena, int posicion, string nombreInicio, string nombreFin, StringComparison reglaComparacion = StringComparison.OrdinalIgnoreCase)
        {
            string respuesta = "";
            int posicionInicio = cadena.IndexOf(nombreInicio, posicion, reglaComparacion);
            if (posicionInicio > -1)
            {
                posicionInicio += nombreInicio.Length;
                int posicionFin = cadena.IndexOf(nombreFin, posicionInicio, reglaComparacion);
                if (posicionFin > -1)
                    respuesta = cadena.Substring(posicionInicio, posicionFin - posicionInicio);
            }

            return respuesta;
        }

        public static string[] ExtraerContenidoEntreTag(string cadena, int posicion, string nombreInicio, string nombreFin, StringComparison reglaComparacion = StringComparison.OrdinalIgnoreCase)
        {
            string[]? arrRespuesta = null;
            int posicionInicio = cadena.IndexOf(nombreInicio, posicion, reglaComparacion);
            if (posicionInicio > -1)
            {
                posicionInicio += nombreInicio.Length;
                int posicionFin = cadena.IndexOf(nombreFin, posicionInicio, reglaComparacion);
                if (posicionFin > -1)
                {
                    posicion = posicionFin + nombreFin.Length;
                    arrRespuesta = new string[2];
                    arrRespuesta[0] = posicion.ToString();
                    arrRespuesta[1] = cadena.Substring(posicionInicio, posicionFin - posicionInicio);
                }
            }

            return arrRespuesta;
        }

        /// <summary>
        /// Elimina los espacios em blanco que ubiera adentro de cada separacion y los retorna unidos por el mismo
        /// caracter pero sin espacios
        /// </summary>
        /// <param name="cadenaTexto"></param>
        /// <param name="caracterSeparador"></param>
        /// <returns></returns>
        public static string EliminarEspaciosEnBlanco(string cadenaTexto, string caracterSeparador)
        {
            string cadenaSinEspaciosEnblanco = string.Empty;
            string[] arrTexto = cadenaTexto.Trim().Split(caracterSeparador, StringSplitOptions.None);
            if (arrTexto.Length > 0)
            {
                foreach (var item in arrTexto)
                {
                    cadenaSinEspaciosEnblanco += item.Trim() + caracterSeparador.ToString();
                }
                cadenaSinEspaciosEnblanco = cadenaSinEspaciosEnblanco.Substring(0, cadenaSinEspaciosEnblanco.Length - 1);
            }
            else
                cadenaSinEspaciosEnblanco = cadenaTexto;

            return cadenaSinEspaciosEnblanco;

        }

        public static string[] RetornarItemsDeCadena(string cadenaTexto, string nombreInicio, string nombreFin)
        {
            string[] arrResultados = { "" };

            string[] arrTemp = cadenaTexto.Split(nombreFin, StringSplitOptions.None);
            if (arrTemp.Length > 0)
            {
                arrResultados = arrTemp;
                int i = 0;
                foreach (var item in arrResultados)
                {
                    arrResultados[i] = item.Replace(nombreInicio, "").Trim();
                    i++;
                }
                arrResultados = arrResultados.Where(x => x != string.Empty).ToArray();
            }
            else
                arrResultados[0] = cadenaTexto;

            return arrResultados;
        }
    }
}
