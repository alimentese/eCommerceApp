using Core.Entities;
namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetProductsAsync(); // don't have add/remove functions
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync(); 
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync(); 
                

    }
}