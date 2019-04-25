using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Kitchen.Api.Exceptions
{
    public abstract class BaseException : Exception
    {
        public abstract HttpStatusCode Status { get; }
    }
}
