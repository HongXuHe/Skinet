using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Skinet.API.Dtos;
using Skinet.Core.Interfaces;

namespace Skinet.API.Controllers
{
    public class ProductTypesController:BaseApiController
    {
        private readonly IProductTypeRepo _typeRepo;
        private readonly IMapper _mapper;

        public ProductTypesController(IProductTypeRepo typeRepo, IMapper mapper)
        {
            _typeRepo = typeRepo;
            _mapper = mapper;
        }
        [HttpGet("")]
        [ProducesResponseType(typeof(List<ProductTypeToReturnDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetProductBrands()
        {
            var types = await _typeRepo.GetEntitiesAsync();
            if (types != null && types.Count > 0) {
                return Ok(_mapper.Map<List<ProductTypeToReturnDto>>(types));
            }

            return NotFound("no types found");
        }
    }
}
