using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL;

namespace BLL
{
    public class SongsContainerService : Service, ISongsContainerService
    {
        public SongsContainerService(IUnitOfWork unit, IMapper mapper) : base(unit, mapper)
        {
        }

        public async Task RemoveSongsContainerAsync(int id)
        {
            var container = await unit.SongsContainerRepository.GetAsync(id);
            if (container == null)
                throw new NotFoundException();
            await unit.SongsContainerRepository.RemoveAsync(id);
        }

        public async Task<SongsContainerDTO> GetSongsContainerAsync(int id, string userId)
        {
            var container = await unit.SongsContainerRepository.GetAsync(id);
            var user = await unit.UserRepository.GetUserByIdAsync(userId);
            if (container == null)
                throw new NotFoundException();
            var containerDTO = mapper.Map<SongsContainerDTO>(container);
            containerDTO.Author = mapper.Map<UserDTO>(container.Author);
            containerDTO.LikedByCurrentUser = user.Albums.Contains(container) || user.Playlists.Contains(container);
            return containerDTO;
        }

        private SongsContainerDTO ConvertSongsContainerToDTO(SongsContainer songsContainer, User user)
        {
            var containerDTO = mapper.Map<SongsContainerDTO>(songsContainer);
            containerDTO.Author = mapper.Map<UserDTO>(songsContainer.Author);
            containerDTO.LikedByCurrentUser = user.Albums.Contains(songsContainer) || user.Playlists.Contains(songsContainer);
            return containerDTO;
        }

        public async Task<IEnumerable<SongsContainerDTO>> GetUserPlaylistsAsync(string userId)
        {
            var user = await unit.UserRepository.GetUserByIdAsync(userId);
            List<SongsContainerDTO> playlists = new List<SongsContainerDTO>();
            foreach (SongsContainer playlist in user.Playlists)
                playlists.Add(ConvertSongsContainerToDTO(playlist, user));
            return playlists;
        }

        public async Task<IEnumerable<SongsContainerDTO>> GetUserAlbumsAsync(string userId)
        {
            var user = await unit.UserRepository.GetUserByIdAsync(userId);
            List<SongsContainerDTO> albums = new List<SongsContainerDTO>();
            foreach (SongsContainer album in user.Albums)
                albums.Add(ConvertSongsContainerToDTO(album, user));
            return albums;
        }

        public async Task<IEnumerable<SongsContainerDTO>> GetSongsContainersByGenreAsync(string genre, string userId)
        {
            var containers = await unit.SongsContainerRepository.GetSongContainersByGenreAsync(genre);
            var user = await unit.UserRepository.GetUserByIdAsync(userId);
            List<SongsContainerDTO> containersDTO = new List<SongsContainerDTO>();
            foreach (SongsContainer container in containers)
                containersDTO.Add(ConvertSongsContainerToDTO(container, user));
            return containersDTO;
        }

        public async Task<IEnumerable<SongsContainerDTO>> GetAlbumsByAuthorAsync(string authorId, string userId)
        {
            var containers = await unit.SongsContainerRepository.GetAllSongsContainersAsync();
            var user = await unit.UserRepository.GetUserByIdAsync(userId);
            return containers.Where(x => x.Author.Id == authorId).Select(y => ConvertSongsContainerToDTO(y, user));
        }

        public async Task<IEnumerable<SongsContainerDTO>> GetPlaylistsByFollowingsAsync(string userId)
        {
            var user = await unit.UserRepository.GetUserByIdAsync(userId);
            var playlists = user.Followings.SelectMany(x => x.Playlists)
                .Distinct()
                .Select(x => ConvertSongsContainerToDTO(x, user));
            return playlists;
        }

        public async Task<IEnumerable<SongsContainerDTO>> GetSongsContainersByNameAsync(string name, string userId)
        {
            var user = await unit.UserRepository.GetUserByIdAsync(userId);
            var containers = await unit.SongsContainerRepository.GetSongsContainersByNameAsync(name);
            return containers.Select(x => ConvertSongsContainerToDTO(x, user));
        }

        public async Task AddSongToPlaylistAsync(int playlistId, int songId, string userId)
        {
            var user = await unit.UserRepository.GetUserByIdAsync(userId);
            var song = await unit.SongRepository.GetAsync(songId);
            var playlist = user.Playlists.FirstOrDefault(x => x.Id == playlistId);

            if (song == null)
                throw new NotFoundException();

            if (playlist == null)
                throw new NotFoundException("Current user does not contain this playlist");

            playlist.Songs.Add(song);
            await unit.SongsContainerRepository.UpdateAsync(playlist);
        }

        public async Task SlapLikeAsync(int songsContainerId, string userId)
        {
            var user = await unit.UserRepository.GetUserByIdAsync(userId);
            var container = await unit.SongsContainerRepository.GetAsync(songsContainerId);
            container.Likes.Add(user);
            await unit.SongsContainerRepository.UpdateAsync(container);
        }

        public async Task<IEnumerable<string>> GetGenresBySongsContainerAsync(int id)
        {
            var container = await unit.SongsContainerRepository.GetAsync(id);
            return container.Genres.Select(x => x.Name);
        }

        public async Task AddAlbumAsync(SongsContainerDTO item)
        {
            var container = mapper.Map<SongsContainer>(item);
            var author = await unit.UserRepository.GetUserByIdAsync(item.Author.Id);
            container.Author = author;
            author.Albums.Add(container);
            await unit.UserRepository.UpdateUserAsync(author);
        }

        public async Task AddPlaylistAsync(SongsContainerDTO item)
        {
            var container = mapper.Map<SongsContainer>(item);
            var author = await unit.UserRepository.GetUserByIdAsync(item.Author.Id);
            container.Author = author;
            author.Playlists.Add(container);
            await unit.UserRepository.UpdateUserAsync(author);
        }
    }
}
