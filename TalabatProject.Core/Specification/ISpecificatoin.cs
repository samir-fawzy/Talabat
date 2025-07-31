using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TalabatProject.Core.Entity;

namespace TalabatProject.Core.Specification
{
    public interface ISpecificatoin<T> where T : BaseEntity
    {
        Expression<Func<T,bool>> Criteria { get; set; }
        List<Expression<Func<T,object>>> Includes { get; set; }
        Expression<Func<T, object>> OrderBy { get; set; }
        Expression<Func<T, object>> OrderByDescending { get; set; }
        int Skip { get; set; }
        int Take { get; set; }
        bool IsBagination { get; set; }
    }
}
