using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class LookupCodeNotFoundException : Exception
    {
        public LookupCodeNotFoundException(Guid lookupCodeId)
            : base($"El LookupCode con el id: {lookupCodeId} no existe en la base de datos.")
        {            
        }
    }
}
