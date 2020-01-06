using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface ISongsContainerRepository : IRepository<SongsContainer, int>
    {
        Task<IEnumerable<SongsContainer>> GetSongContainersByGenreAsync(string genre);
        Task<IEnumerable<SongsContainer>> GetAllSongsContainersAsync();
        Task<IEnumerable<SongsContainer>> GetSongsContainersByNameAsync(string name);

        Task<IEnumerable<Genre>> GetGenresAsync();
    }
}
