using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductsWithFiltersForCountSpecification(ProductSpecParams param) : base(x =>
            (string.IsNullOrEmpty(param.Search) || x.Name.ToLower().Contains(param.Search)) &&
            (!param.BrandId.HasValue || x.ProductBrandId == param.BrandId) &&
            (!param.TypeId.HasValue || x.ProductTypeId == param.TypeId))
        {

        }
    }
}