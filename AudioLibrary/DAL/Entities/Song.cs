using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Song
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Listens { get; set; }

        public string AudioPath { get; set; }
        public virtual SongsContainer Album { get; set; }
        public virtual List<SongsContainer> Playlists { get; set; }
        public virtual List<User> Likes { get; set; }
        public virtual List<Commentary> Commentaries { get; set; }

        public Song()
        {
            Playlists = new List<SongsContainer>();
            Likes = new List<User>();
            Commentaries = new List<Commentary>();
        }
    }
}
