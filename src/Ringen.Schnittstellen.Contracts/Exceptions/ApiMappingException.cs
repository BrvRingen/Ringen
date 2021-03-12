﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ringen.Schnittstellen.Contracts.Exceptions
{
    public class ApiMappingException : Exception
    {
        public ApiMappingException()
        {
            
        }

        public ApiMappingException(string nachricht) : base(nachricht)
        {
            
        }

        public ApiMappingException(string nachricht, Exception innerException) : base(nachricht, innerException)
        {

        }
    }
}