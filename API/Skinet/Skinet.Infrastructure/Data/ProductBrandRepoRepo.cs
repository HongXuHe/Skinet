using System;
using System.Collections.Generic;
using System.Text;
using Skinet.Core.Entities;
using Skinet.Core.Interfaces;

namespace Skinet.Infrastructure.Data
{
    public class ProductBrandRepoRepo : BaseRepo<ProductBrand>, IProductBrandRepo
    {
        private readonly StoreContext _context;

        public ProductBrandRepoRepo(StoreContext context):base(context)
        {
            _context = context;
        }
    }
}
