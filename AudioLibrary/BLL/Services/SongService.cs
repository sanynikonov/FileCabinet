using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL;

namespace BLL
{
    public class SongService : Service, ISongService
    {
        public SongService(IUnitOfWork unit, IMapper mapper) : base(unit, mapper)
        {
        }


        public async Task AddSongAsync(SongDTO item)
        {
            var song = mapper.Map<Song>(item);
            song.Album = await unit.SongsContainerRepository.GetAsync(item.AlbumId);
            await unit.SongRepository.AddAsync(song);
        }

        public async Task UpdateSongAsync(SongDTO item)
        {
            var song = mapper.Map<Song>(item);
            song.Album = await unit.SongsContainerRepository.GetAsync(item.AlbumId);
            await unit.SongRepository.UpdateAsync(song);
        }

        public async Task RemoveSongAsync(int id)
        {
            var song = await unit.SongRepository.GetAsync(id);
            if (song == null)
                throw new NotFoundException();
            await unit.SongRepository.RemoveAsync(id);
        }

        public async Task<SongDTO> GetSongAsync(int id, string userId)
        {
            var song = await unit.SongRepository.GetAsync(id);
            if (song == null)
                throw new NotFoundException();
            var user = await unit.UserRepository.GetUserByIdAsync(userId);
            var songDTO = mapper.Map<SongDTO>(song);
            songDTO.LikedByCurrentUser = user.LikedSongs.Contains(song);
            return songDTO;
        }

        public async Task<IEnumerable<SongDTO>> GetSongsByAlbumIdAsync(int albumId, string userId)
        {
            var album = await unit.SongsContainerRepository.GetAsync(albumId);
            if (album == null)
                throw new NotFoundException();
            List<SongDTO> songs = new List<SongDTO>();
            foreach (Song song in album.Songs)
                songs.Add(await GetSongAsync(song.Id, userId));
            return songs;
        }

        public async Task<IEnumerable<SongDTO>> GetSongsByNameAsync(string songName, string userId)
        {
            var songs = await unit.SongRepository.GetSongsByNameAsync(songName);
            List<SongDTO> songsDTO = new List<SongDTO>();
            foreach (Song song in songs)
                songsDTO.Add(await GetSongAsync(song.Id, userId));
            return songsDTO;
        }

        public async Task<IEnumerable<SongDTO>> GetLikedSongsAsync(string userId)
        {
            var user = await unit.UserRepository.GetUserByIdAsync(userId);
            if (user == null)
                throw new NotFoundException();
            List<SongDTO> songsDTO = new List<SongDTO>();
            foreach (Song song in user.LikedSongs)
                songsDTO.Add(await GetSongAsync(song.Id, userId));
            return songsDTO;
        }

        public async Task SlapLikeAsync(int songId, string userId)
        {
            var song = await unit.SongRepository.GetAsync(songId);
            if (song == null)
                throw new NotFoundException();
            var user = await unit.UserRepository.GetUserByIdAsync(userId);

            if (song.Likes.Contains(user))
                song.Likes.Remove(user);
            else
                song.Likes.Add(user);

            await unit.SongRepository.UpdateAsync(song);
        }

        public async Task AddListenAsync(int id)
        {
            var song = await unit.SongRepository.GetAsync(id);
            if (song == null)
                throw new NotFoundException();
            song.Listens++;

            await unit.SongRepository.UpdateAsync(song);
        }

    }
}
