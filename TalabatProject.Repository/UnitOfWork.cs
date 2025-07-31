using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatProject.Core;
using TalabatProject.Core.Entity;
using TalabatProject.Core.Interfaces;
using TalabatProject.Repository.Data;

namespace TalabatProject.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _Repository;
        private readonly StoreContext context;

        public UnitOfWork(StoreContext context)
        {
            _Repository = new Hashtable();
            this.context = context;
        }
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity).Name;
            if(!_Repository.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(context);
                _Repository.Add(type, repository);
            }
            return _Repository[type] as IGenericRepository<TEntity>;
        }
        public async Task<int> Complete()
            => await context.SaveChangesAsync();

        public async ValueTask DisposeAsync()
        => await context.DisposeAsync();


    }
}
