﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.Domain.Sale.Interfaces;

namespace Erp.Domain.Sale.Repositories
{
    public class GenericRepository<TContext, TEntity> : IGenericRepository<TEntity>
        where TContext : IDbContext
        where TEntity : class
    {
        public ErpSaleDbContext Context;

        /// <summary>
        /// Constructor that takes a context
        /// </summary>
        /// <param name="context">An established data context</param>
        public GenericRepository(TContext context)
        {
            Context = new ErpSaleDbContext();
            //Context = context as ErpSaleDbContext;
        }

        public void Renew()
        {
            Context = new ErpSaleDbContext();
        }

        public IQueryable<TEntity> Select()
        {
            return Context.Set<TEntity>().AsQueryable();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().AsEnumerable();
        }

        public IEnumerable<TEntity> Where(Func<TEntity, bool> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public TEntity GetSingle(Func<TEntity, bool> predicate)
        {
            return Context.Set<TEntity>().Single(predicate);
        }

        public TEntity GetFirst(Func<TEntity, bool> predicate)
        {
            return Context.Set<TEntity>().First(predicate);
        }

        public void Add(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentException("object is null");

            Context.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentException("object is null");

            Context.Set<TEntity>().Remove(entity);
        }

        public void Attach(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentException("object is null");

            Context.Set<TEntity>().Attach(entity);
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                Context.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
