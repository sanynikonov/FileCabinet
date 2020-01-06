using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface ISongRepository : IRepository<Song, int>
    {
        Task<IEnumerable<Song>> GetSongsByNameAsync(string songName);
        Task<IEnumerable<Song>> GetSongsByAlbumIdAsync(int albumId);
    }
}
