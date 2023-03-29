using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams param)
                : base(x =>
            (string.IsNullOrEmpty(param.Search) || x.Name.ToLower().Contains(param.Search)) &&
                (!param.BrandId.HasValue || x.ProductBrandId == param.BrandId) && (!param.TypeId.HasValue || x.ProductTypeId == param.TypeId))
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
            AddOrderBy(p => p.Name);
            ApplyPaging(param.PageSize * (param.PageIndex - 1), param.PageSize);

            if (!string.IsNullOrEmpty(param.Sort))
            {

                switch (param.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
        }


        public ProductsWithTypesAndBrandsSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}