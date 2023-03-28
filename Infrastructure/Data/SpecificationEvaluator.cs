using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,
            ISpecification<TEntity> spec)
        {
            var query = inputQuery;

            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            // iterate over the "Includes" list of the specification and apply each navigation property to the query using the "Include" method.
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            // returns the modified query with the search criteria and navigation properties applied
            return query;
        }
    }
}