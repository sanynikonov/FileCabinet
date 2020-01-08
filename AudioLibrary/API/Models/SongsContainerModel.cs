using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API
{
    public class SongsContainerModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateOfCreation { get; set; }

        public string CoverPath { get; set; }

        public UserModel Author { get; set; }
        public int LikesAmount { get; set; }
        public bool LikedByCurrentUser { get; set; }
    }
}