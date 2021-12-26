using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skinet.API.Dtos;
using Skinet.Core.Interfaces;
using Skinet.Core.Specifications;
using Skinet.Infrastructure.Data;


namespace Skinet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController:ControllerBase
    {
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;


        public ProductsController(IProductRepo productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetProducts()
        {
            var spec =new ProductsWithTypesAndBrandsSpecification();
            var products = await _productRepo.GetEntitiesAsync(spec);
            return Ok(_mapper.Map<List<ProductToReturnDto>>(products));
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(x=>x.Id==id);
            var result = await _productRepo.GetEntityWithSpec(spec);
            if (result != null)
            {
                return Ok(_mapper.Map<ProductToReturnDto>(result));
            }
            return NotFound($"Product with id ={id} not exists");
        }
    }
}
