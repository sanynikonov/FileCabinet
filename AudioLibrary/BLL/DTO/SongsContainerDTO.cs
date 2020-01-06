using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SongsContainerDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateOfCreation { get; set; }

        public string CoverPath { get; set; }

        public UserDTO Author { get; set; }
        public int LikesAmount { get; set; }
        public bool LikedByCurrentUser { get; set; }
    }
}
