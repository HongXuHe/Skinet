using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skinet.API.Errors
{
    public class ApiException:ApiResponse
    {
        public string Details { get; }

        public ApiException(int statusCode,string message =null,string details=null):base(statusCode,message)
        {
            Details = details;
        }
    }
}
