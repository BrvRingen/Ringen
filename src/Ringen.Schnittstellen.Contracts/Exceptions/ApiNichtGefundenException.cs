using System;

namespace Ringen.Schnittstellen.Contracts.Exceptions
{
    public class ApiNichtGefundenException : Exception
    {
        public ApiNichtGefundenException(string nachricht) : base(nachricht)
        {
            
        }
    }
}
