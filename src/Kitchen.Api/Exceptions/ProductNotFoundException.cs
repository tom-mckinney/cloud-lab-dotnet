using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Kitchen.Api.Exceptions
{
    public class ProductNotFoundException : BaseException
    {
        public override HttpStatusCode Status => HttpStatusCode.NotFound;
    }
}
