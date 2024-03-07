using System.ComponentModel.DataAnnotations;

namespace Entities.Validations
{
    public class RucValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            // Verificar si el valor es nulo o no es una cadena
            if (value == null || !(value is string))
            {
                return false;
            }

            string ruc = (string)value;

            // Verificar si el RUC tiene 11 dígitos numéricos
            if (ruc.Length != 11 || !EsNumero(ruc))
            {
                return false;
            }

            return true;
        }

        private bool EsNumero(string value)
        {
            foreach (char c in value)
            {
                if (!Char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
