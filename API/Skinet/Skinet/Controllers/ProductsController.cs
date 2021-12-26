using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
       


        public ProductsController(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetProducts()
        {
            var spec =new ProductsWithTypesAndBrandsSpecification();
            var products = await _productRepo.GetEntitiesAsync(spec);
            return Ok(products);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(x=>x.Id==id);
            var result = await _productRepo.GetEntityWithSpec(spec);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound($"Product with id ={id} not exists");
        }
    }
}
