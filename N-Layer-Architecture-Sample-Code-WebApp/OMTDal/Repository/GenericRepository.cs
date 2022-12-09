using Microsoft.EntityFrameworkCore;
using OMTDal.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace OMTDal.Repository
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected internal OmtContext Context { get; set; }
        internal DbSet<TEntity> dbSet;
        protected GenericRepository(OmtContext context)
        {
            this.Context = context; 
            dbSet = this.Context.Set<TEntity>();    
        }
        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
        {
            return dbSet.Where(expression); 
        }
        public void Add(TEntity entity)
        {
            dbSet.Add(entity);  
        }
        public void Delete(TEntity entity)
        {
           dbSet.Remove(entity);
        }
        public TEntity Find(Expression<Func<TEntity, bool>> expression)
        {
            return dbSet.Find(expression);  
        }
        public IQueryable<TEntity> GetAll()
        {
            return dbSet.AsQueryable();
        }
        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
        }
    }
}
