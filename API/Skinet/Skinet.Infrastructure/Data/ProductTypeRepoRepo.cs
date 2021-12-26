using System;
using System.Collections.Generic;
using System.Text;
using Skinet.Core.Entities;
using Skinet.Core.Interfaces;

namespace Skinet.Infrastructure.Data
{
    public class ProductTypeRepoRepo : BaseRepo<ProductType>, IProductTypeRepo
    {
        private readonly StoreContext _context;

        public ProductTypeRepoRepo(StoreContext context) : base(context)
        {
            _context = context;
        }
    }
}
