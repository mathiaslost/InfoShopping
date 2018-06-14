using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UI.Web.Models;

namespace UI.Web.Services
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly Data.ApplicationDbContext Context;

        protected readonly Microsoft.EntityFrameworkCore.DbSet<T> DbSet;

        public object CidadeModel => throw new NotImplementedException();

        public GenericRepository(Data.ApplicationDbContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }

        public virtual T Get(object id)
        {
            return DbSet.Find(id);
        }

        public virtual List<T> GetAll(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = DbSet;

            foreach (var includeProperty in includeProperties)
                queryable = queryable.Include(includeProperty);

            return queryable.ToList();
        }

        public virtual bool Update(object id, T updated)
        {
            Context.Update(updated);
            Context.SaveChanges();

            return true;
        }

        public virtual bool Remove(object id)
        {
            var resource = Get(id);
            DbSet.Remove(resource);
            Context.SaveChanges();

            return true;
        }

        public virtual async Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = DbSet;

            foreach (var includeProperty in includeProperties)
                queryable = queryable.Include(includeProperty);

            return await queryable.ToListAsync();
        }

        public virtual async Task<T> GetAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<bool> RemoveAsync(object id)
        {
            var resource = await GetAsync(id);
            DbSet.Remove(resource);
            await Context.SaveChangesAsync();

            return true;
        }

        public virtual async Task<bool> UpdateAsync(object id, T updated)
        {
            try
            {
                Context.Update(updated);
                await Context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public virtual async Task<bool> InsertAsync(T insert)
        {
            DbSet.Add(insert);
            await Context.SaveChangesAsync();

            return true;
        }

        public virtual bool Insert(T insert)
        {
            DbSet.Add(insert);
            Context.SaveChanges();

            return true;
        }

        public virtual bool Exists(object id)
        {
            return Get(id) != null;
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, CidadeModel cidadeModel)
        {
            throw new NotImplementedException();
        }
    }
}
