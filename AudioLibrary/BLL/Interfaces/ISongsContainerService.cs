using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface ISongsContainerService
    {
        Task AddSongsContainerAsync(SongsContainerDTO item);
        Task RemoveSongsContainerAsync(int id);

        Task<SongsContainerDTO> GetSongsContainerAsync(int id, string userId);
        Task<IEnumerable<SongsContainerDTO>> GetSongContainersByGenreAsync(string genre, string userId);
        Task<IEnumerable<SongsContainerDTO>> GetSongsContainersByNameAsync(string name, string userId);
        Task<IEnumerable<SongsContainerDTO>> GetAlbumsByAuthorAsync(string authorId, string userId);
        Task<IEnumerable<SongsContainerDTO>> GetUserPlaylistsAsync(string userId);
        Task<IEnumerable<SongsContainerDTO>> GetUserAlbumsAsync(string userId);
        Task<IEnumerable<SongsContainerDTO>> GetPlaylistsByFollowingsAsync(string userId);

        Task<IEnumerable<string>> GetGenresBySongsContainer(int id);
        Task AddSongToPlaylistAsync(int playlistId, int songId);
        Task SlapLikeAsync(int songsContainerId, string userId);
    }
}
