using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TalabatProject.Core.Entity;
using TalabatProject.Core.Specification;

namespace TalabatProject.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecificatoin<T> spec);
        Task<T> GetByIdWithSpecAsync(ISpecificatoin<T> spec);
        Task<int> GetTotalCountAsync(ISpecificatoin<T> spec);
        Task<IReadOnlyList<T>> GetAllAsync(); 
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
