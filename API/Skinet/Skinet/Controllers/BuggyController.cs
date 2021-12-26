using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Skinet.API.Controllers
{
    public class BuggyController:BaseApiController
    {
        [HttpGet("")]
        public IActionResult NotFoundResult()
        {
            throw new Exception();
        }
    }
}
