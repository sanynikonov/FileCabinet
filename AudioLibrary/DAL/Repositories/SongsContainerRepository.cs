using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SongsContainerRepository : Repository<SongsContainer, int>, ISongsContainerRepository
    {
        public SongsContainerRepository(AudioLibraryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<SongsContainer>> GetAllSongsContainersAsync()
        {
            return await context.SongsContainers.ToListAsync();
        }

        public async Task<IEnumerable<Genre>> GetGenresAsync()
        {
            return await context.Genres.ToListAsync();
        }

        public async Task<IEnumerable<SongsContainer>> GetSongContainersByGenreAsync(string genreName)
        {
            var genre = await context.Genres.Where(x => x.Name.ToLower().Contains(genreName.ToLower())).FirstOrDefaultAsync();
            return genre.SongsContainers;
        }

        public async Task<IEnumerable<SongsContainer>> GetSongsContainersByNameAsync(string name)
        {
            return await context.SongsContainers.Where(x => x.Title.ToLower().Contains(name.ToLower())).ToListAsync();
        }
    }
}
