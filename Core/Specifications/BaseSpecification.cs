using System.Linq.Expressions;

namespace Core.Specifications
{
    ///<summary>
    /// The class contains properties and methods to define search criteria for database queries.
    ///</summary>
    public class BaseSpecification<T> : ISpecification<T>
    {
        // Represents the search criteria for the query.
        public Expression<Func<T, bool>> Criteria { get; }

        // Represents the property on which we want to sort the results in ascending order
        public Expression<Func<T, object>> OrderBy { get; private set; }

        // Represents the property on which we want to sort the results in descending order.
        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        // Objects and is used to specify navigation properties that should be included in the query.
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        // Represents the number of records to take from the result.
        public int Take { get; private set; }

        // Represents the number of records to skip from the result.
        public int Skip { get; private set; }

        // Represents whether paging is enabled for the query or not.
        public bool IsPagingEnabled { get; private set; }

        public BaseSpecification()
        {
        }

        ///<summary>
        /// Sets the "Criteria" property to the given value.
        ///</summary>
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }


        ///<summary>
        /// Allows for additional navigation properties to be added to the query.
        ///</summary>
        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        ///<summary>
        /// Specifies the property on which we want to sort the results in ascending order.
        ///</summary>
        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        ///<summary>
        /// Specifies the property on which we want to sort the results in descending order.
        ///</summary>
        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }

        ///<summary>
        /// Applies paging to the query.
        ///</summary>
        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }
}