using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL;

namespace BLL
{
    public class CommentaryService : Service, ICommentaryService
    {
        public CommentaryService(IUnitOfWork unit, IMapper mapper) : base(unit, mapper)
        {
        }

        public async Task AddCommentaryAsync(CommentaryDTO item)
        {
            var commentary = mapper.Map<Commentary>(item);
            commentary.Song = await unit.SongRepository.GetAsync(item.SongId);
            commentary.User = await unit.UserRepository.GetUserByIdAsync(item.User.Id);
            await unit.CommentaryRepository.AddAsync(commentary);
            await unit.SaveChangesAsync();
        }

        public async Task UpdateCommentaryAsync(CommentaryDTO item)
        {
            var commentary = mapper.Map<Commentary>(item);
            commentary.Song = await unit.SongRepository.GetAsync(item.SongId);
            commentary.User = await unit.UserRepository.GetUserByIdAsync(item.User.Id);
            await unit.CommentaryRepository.UpdateAsync(commentary);
            await unit.SaveChangesAsync();
        }

        public async Task RemoveCommentaryAsync(int id)
        {
            await unit.CommentaryRepository.RemoveAsync(id);
            await unit.SaveChangesAsync();
        }

        public async Task<CommentaryDTO> GetCommentaryAsync(int id)
        {
            var commentary = await unit.CommentaryRepository.GetAsync(id);
            if (commentary == null)
                throw new NotFoundException();
            var commentaryDTO = mapper.Map<CommentaryDTO>(commentary);
            commentaryDTO.User = mapper.Map<UserDTO>(commentary.User);
            return commentaryDTO;
        }

        public async Task<IEnumerable<CommentaryDTO>> GetCommentariesBySongIdAsync(int songId)
        {
            var song = await unit.SongRepository.GetAsync(songId);
            var commentaries = new List<CommentaryDTO>();
            foreach (var commentary in song.Commentaries)
                commentaries.Add(await GetCommentaryAsync(commentary.Id));
            return commentaries;
        }
    }
}
