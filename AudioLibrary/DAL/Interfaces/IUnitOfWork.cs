using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IUnitOfWork : IDisposable
    {
        ICommentaryRepository CommentaryRepository { get; }
        ISongRepository SongRepository { get; }
        ISongsContainerRepository SongsContainerRepository { get; }
        IUserRepository UserRepository { get; }

        Task<int> SaveChangesAsync();
    }
}
