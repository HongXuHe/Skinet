using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skinet.API.Dtos;
using Skinet.Core.Interfaces;

namespace Skinet.API.Controllers
{
    public class ProductBrandsController:BaseApiController
    {
        private readonly IProductBrandRepo _brandRepo;
        private readonly IMapper _mapper;

        public ProductBrandsController(IProductBrandRepo brandRepo, IMapper mapper)
        {
            _brandRepo = brandRepo;
            _mapper = mapper;
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(List<ProductBrandToReturnDto>),(int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetProductBrands()
        {
            var brands =await _brandRepo.GetEntitiesAsync();
            if (brands != null && brands.Count > 0)
            {
                return Ok(_mapper.Map<List<ProductBrandToReturnDto>>(brands));
            }

            return NotFound("no brands found");
        }
    }
}
