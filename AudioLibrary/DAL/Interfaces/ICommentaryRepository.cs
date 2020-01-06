using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface ICommentaryRepository : IRepository<Commentary, int>
    {
        Task<IEnumerable<Commentary>> GetCommentariesBySongIdAsync(int songId);
    }
}
