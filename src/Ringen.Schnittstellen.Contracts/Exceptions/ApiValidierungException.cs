using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ringen.Schnittstellen.Contracts.Exceptions
{
    public class ApiValidierungException: Exception
    {
        public List<KeyValuePair<string, string>> ValidierungsFehler { get; private set; }

        public ApiValidierungException(List<KeyValuePair<string, string>> validierungsFehler)
        {
            ValidierungsFehler = validierungsFehler;
        }
    }
}
