using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skinet.Core.Interfaces;

namespace Skinet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductBrandsController:ControllerBase
    {
        private readonly IProductBrandRepo _brandRepo;

        public ProductBrandsController(IProductBrandRepo brandRepo)
        {
            _brandRepo = brandRepo;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetProductBrands()
        {
            var brands =await _brandRepo.GetEntitiesAsync();
            if (brands != null && brands.Count > 0)
            {
                return Ok(brands);
            }

            return NotFound("no brands found");
        }
    }
}
