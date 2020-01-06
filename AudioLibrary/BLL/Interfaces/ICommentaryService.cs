using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface ICommentaryService
    {
        Task<CommentaryDTO> GetCommentaryAsync(int id);
        Task AddCommentaryAsync(CommentaryDTO item);
        Task RemoveCommentaryAsync(int id);
        Task<IEnumerable<CommentaryDTO>> GetCommentariesBySongIdAsync(int songId);
    }
}
