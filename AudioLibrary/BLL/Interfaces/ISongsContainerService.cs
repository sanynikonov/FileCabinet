using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface ISongsContainerService : IDisposable
    {
        Task AddSongsContainerAsync(SongsContainerDTO item);
        Task RemoveSongsContainerAsync(int id);

        Task<SongsContainerDTO> GetSongsContainerAsync(int id, string userId);
        Task<IEnumerable<SongsContainerDTO>> GetSongsContainersByGenreAsync(string genre, string userId);
        Task<IEnumerable<SongsContainerDTO>> GetSongsContainersByNameAsync(string name, string userId);
        Task<IEnumerable<SongsContainerDTO>> GetAlbumsByAuthorAsync(string authorId, string userId);
        Task<IEnumerable<SongsContainerDTO>> GetUserPlaylistsAsync(string userId);
        Task<IEnumerable<SongsContainerDTO>> GetUserAlbumsAsync(string userId);
        Task<IEnumerable<SongsContainerDTO>> GetPlaylistsByFollowingsAsync(string userId);

        Task<IEnumerable<string>> GetGenresBySongsContainerAsync(int id);
        Task AddSongToPlaylistAsync(int playlistId, int songId, string userId);
        Task SlapLikeAsync(int songsContainerId, string userId);
    }
}
