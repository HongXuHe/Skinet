using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skinet.API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode,string message =null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            switch (statusCode)
            {
                case 400:
                    return "A bad request";

                case 401:
                    return "Authorized error";
                    
                case 404:
                    return "not found error";
                case 500:
                    return "internal error";
                default: return null;
            }
        }
    }
}
