﻿using System;
using System.Collections.Generic;
using System.Text;
using Skinet.Core.Entities;

namespace Skinet.Core.Specifications
{
    public class ProductWithFiltersForCountSpecification: BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecParams productSpecParams)
            : base(x => (string.IsNullOrEmpty(productSpecParams.Search)||x.Name.ToLower().Contains(productSpecParams.Search)) &&
                (!productSpecParams.BrandId.HasValue || x.ProductBrandId == productSpecParams.BrandId) &&
                        (!productSpecParams.TypeId.HasValue || x.ProductTypeId == productSpecParams.TypeId))
        {
            
        }
    }
}
