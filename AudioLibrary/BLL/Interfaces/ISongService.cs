using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface ISongService : IDisposable
    {
        Task AddSongAsync(SongDTO item);
        Task RemoveSongAsync(int id);

        Task<SongDTO> GetSongAsync(int id, string userId);
        Task<IEnumerable<SongDTO>> GetSongsByNameAsync(string songName, string userId);
        Task<IEnumerable<SongDTO>> GetSongsByAlbumIdAsync(int albumId, string userId);
        Task<IEnumerable<SongDTO>> GetLikedSongsAsync(string userId);

        Task AddListenAsync(int id);
        Task SlapLikeAsync(int songId, string userId);
    }
}
