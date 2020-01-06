using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class User : IdentityUser
    {
        //[Key]
        //[ForeignKey("UserLogin")]
        //public string Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string PhotoPath { get; set; }

        public virtual Country Country { get; set; }

        public virtual List<User> Followers { get; set; }
        public virtual List<User> Followings { get; set; }

        public virtual List<Song> LikedSongs { get; set; }
        public virtual List<SongsContainer> Playlists { get; set; }
        public virtual List<SongsContainer> Albums { get; set; }
        //public virtual UserLogin UserLogin { get; set; }

        public User()
        {
            Followers = new List<User>();
            Followings = new List<User>();
            LikedSongs = new List<Song>();
            Playlists = new List<SongsContainer>();
            Albums = new List<SongsContainer>();
        }
    }
}
