using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CommentaryRepository : Repository<Commentary, int>, ICommentaryRepository
    {
        public CommentaryRepository(AudioLibraryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Commentary>> GetCommentariesBySongIdAsync(int songId)
        {
            var song = await context.Songs.FindAsync(songId);
            return song.Commentaries;
        }
    }
}
