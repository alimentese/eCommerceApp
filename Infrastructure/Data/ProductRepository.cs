using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    ///<summary>
    /// ProductRepository contains methods to retrieve products, product types, and product brands from the database using the Entity Framework Core.
    ///</summary>
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext context;

        ///<summary>
        /// Constructor of the class takes in an instance of "StoreContext" which is used to access the database..
        ///</summary>
        public ProductRepository(StoreContext context)
        {
            this.context = context;
        }

        ///<summary>
        /// Retrieves a product from the database by its ID, including its related product type and brand using eager loading.
        ///</summary>
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await context.Products
            // Eager loading of navigations properties
            .Include(p => p.ProductType)
            .Include(p => p.ProductBrand)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// Retrieves all products from the database, including their related product type and brand using eager loading.
        /// </summary>
        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await context.Products
            // Eager loading of navigations properties
            .Include(p => p.ProductType)
            .Include(p => p.ProductBrand).ToListAsync();
        }

        /// <summary>
        /// Retrieves all product types from the database.
        /// </summary>
        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await context.ProductTypes.ToListAsync();
        }

        /// <summary>
        /// Retrieves all product brands from the database.
        /// </summary>
        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await context.ProductBrands.ToListAsync();
        }
    }
}