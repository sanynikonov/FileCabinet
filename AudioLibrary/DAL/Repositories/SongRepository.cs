using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SongRepository : Repository<Song, int>, ISongRepository
    {
        public SongRepository(AudioLibraryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Song>> GetSongsByAlbumIdAsync(int albumId)
        {
            var album = await context.SongsContainers.FindAsync(albumId);
            return album.Songs;
        }

        public async Task<IEnumerable<Song>> GetSongsByNameAsync(string songName)
        {
            return await context.Songs.Where(x => x.Name.ToLower().Contains(songName.ToLower())).ToListAsync();
        }
    }
}
