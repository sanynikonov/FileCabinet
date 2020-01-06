using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private AudioLibraryContext context;
        private ICommentaryRepository commentaryRepository;
        private ISongRepository songRepository;
        private IUserRepository userRepository;
        private ISongsContainerRepository songsContainerRepository;

        public UnitOfWork()
        {
            context = new AudioLibraryContext();
        }

        public ICommentaryRepository CommentaryRepository
        {
            get
            {
                if (commentaryRepository == null)
                    commentaryRepository = new CommentaryRepository(context);
                return commentaryRepository;
            }
        }

        public ISongRepository SongRepository
        {
            get
            {
                if (songRepository == null)
                    songRepository = new SongRepository(context);
                return songRepository;
            }
        }

        public ISongsContainerRepository SongsContainerRepository
        {
            get
            {
                if (songsContainerRepository == null)
                    songsContainerRepository = new SongsContainerRepository(context);
                return songsContainerRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(context);
                return userRepository;
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
