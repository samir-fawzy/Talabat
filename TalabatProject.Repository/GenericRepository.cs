using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatProject.Core.Entity;
using TalabatProject.Core.Interfaces;
using TalabatProject.Core.Specification;
using TalabatProject.Repository.Data;

namespace TalabatProject.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext dbContext;

        public GenericRepository(StoreContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(T entity)
            => await dbContext.AddAsync(entity);

        public void Delete(T entity)
            => dbContext.Remove(entity);

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecificatoin<T> spec)
        {
            var products = await ApplySpecification(dbContext.Set<T>(), spec).ToListAsync();
            return products;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await dbContext.Set<T>().FindAsync(id);
            
        }

        public async Task<T> GetByIdWithSpecAsync(ISpecificatoin<T> spec)
        {
            var product = await ApplySpecification(dbContext.Set<T>(), spec).FirstOrDefaultAsync();
            return product;
        }

        public async Task<int> GetTotalCountAsync(ISpecificatoin<T> spec) 
            => await SpecificationEvaluator<T>.GetQuery(dbContext.Set<T>(), spec).CountAsync();

        public void Update(T entity)
            => dbContext.Update(entity);


        private IQueryable<T> ApplySpecification(IQueryable<T> inputQuery,ISpecificatoin<T> spec) 
            => SpecificationEvaluator<T>.GetQuery(inputQuery, spec);

    }
}
