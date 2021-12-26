using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Skinet.Core.Entities;

namespace Skinet.Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification()
        {
            AddInclude(x=>x.ProductBrand);
            AddInclude(x=>x.ProductType);
        }

        public ProductsWithTypesAndBrandsSpecification(Expression<Func<Product, bool>> criteria):base(criteria)
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
        }
    }
}
