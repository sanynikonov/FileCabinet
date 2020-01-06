using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IRepository<TEntity, TKey>
    {
        Task<TEntity> GetAsync(TKey id);
        Task AddAsync(TEntity item);
        Task UpdateAsync(TEntity item);
        Task RemoveAsync(TKey id);
    }
}
