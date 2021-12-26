using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Skinet.Core.Entities;
using Skinet.Core.Interfaces;

namespace Skinet.Infrastructure.Data
{
    public class ProductRepo:BaseRepo<Product>, IProductRepo
    {
        private readonly StoreContext _context;

        public ProductRepo(StoreContext context):base(context)
        {
            _context = context;
        }
      
    }
}
