using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Repository<TEntity, TKey> : IDisposable, IRepository<TEntity, TKey> where TEntity : class
    {
        protected AudioLibraryContext context;
        protected DbSet<TEntity> entities;

        public Repository(AudioLibraryContext context)
        {
            this.context = context;
            entities = context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity item)
        {
            entities.Add(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity item)
        {
            context.Entry(item).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<TEntity> GetAsync(TKey id)
        {
            return await entities.FindAsync(id);
        }

        public async Task RemoveAsync(TKey id)
        {
            var item = entities.Find(id);
            if (item != null)
                entities.Remove(item);
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
