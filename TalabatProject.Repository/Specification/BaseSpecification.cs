using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TalabatProject.Core.Entity;
using TalabatProject.Core.Specification;

namespace TalabatProject.Repository.Specification
{
    public class BaseSpecification<T> : ISpecificatoin<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T,object>> OrderBy { get; set; }
        public Expression<Func<T,object>> OrderByDescending { get; set; }
        public Expression<Func<T,object>> Where { get; set; }
        public int Skip { get; set; }
        public int Take { get ; set; }
        public bool IsBagination { get; set; }

        public BaseSpecification()
        {
            
        }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        protected void AddOrderBy(Expression<Func<T, object>> OrderBy)
        {
            this.OrderBy = OrderBy;
        }
        protected void AddOrderByDescending(Expression<Func<T, object>> OrderByDescending)
        {
            this.OrderByDescending = OrderByDescending;
        }
        protected void ApplyPagination(int skip,int take)
        {
            IsBagination = true;
            Skip = skip;
            Take = take;
        }
        protected void AddWhere(Expression<Func<T,object>> where)
        {
            Where = where;
        }
    }
}
