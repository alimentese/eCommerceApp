using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    ///<summary>
    /// The generic class contains methods to perform basic CRUD operations on a database using Entity Framework Core.
    /// The class uses the Specification pattern to encapsulate search criteria for database queries, allowing for greater flexibility and reusability of code.
    ///</summary>
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext context;
        public GenericRepository(StoreContext context)
        {
            this.context = context;
        }

        ///<summary>
        /// Retrieves an entity from the database by its ID. If the entity type is "Product", it includes related product type and brand using eager loading.
        ///</summary>
        public async Task<T> GetByIdAsync(int id)
        {
            if (typeof(T) == typeof(Product))
            {
                // Eager loading of navigations properties
                return await context.Set<T>()
                .Include(p => (p as Product).ProductType)
                .Include(p => (p as Product).ProductBrand)
                .FirstOrDefaultAsync(p => p.Id == id);
            }
            else
            {
                return await context.Set<T>().FindAsync(id);
            }
        }

        ///<summary>
        /// Retrieves all entities of a given type from the database. If the entity type is "Product", it includes related product type and brand using eager loading.
        ///</summary>
        public async Task<IReadOnlyList<T>> GetListAllAsync()
        {

            if (typeof(T) == typeof(Product))
            {
                return await context.Set<T>()
                .Include(p => (p as Product).ProductType)
                .Include(p => (p as Product).ProductBrand)
                .ToListAsync();
            }
            else
            {
                return await context.Set<T>().ToListAsync();
            }
        }

        ///<summary>
        /// Retrieves a single entity that matches a given specification.
        ///</summary>
        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        ///<summary>
        /// Retrieves a list of entities that match a given specification.
        ///</summary>
        public async Task<IReadOnlyList<T>> GetListAllAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        ///<summary>
        /// Returns a queryable object that includes any specifications passed in as an argument.
        ///</summary>
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(context.Set<T>().AsQueryable(), spec);
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }
    }
}