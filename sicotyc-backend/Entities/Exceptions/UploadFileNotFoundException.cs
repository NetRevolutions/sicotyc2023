using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class UploadFileNotFoundException : Exception
    {
        public UploadFileNotFoundException(string fileType)
            : base($"El tipo de archivo: {fileType} para subir no existe")
        { }
    }
}
