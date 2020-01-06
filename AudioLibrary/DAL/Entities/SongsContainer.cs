using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SongsContainer
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateOfCreation { get; set; }

        public string CoverPath { get; set; }

        public virtual List<Song> Songs { get; set; }
        public virtual User Author { get; set; }
        public virtual List<User> Likes { get; set; }
        public virtual List<Genre> Genres { get; set; }

        public SongsContainer()
        {
            Songs = new List<Song>();
            Likes = new List<User>();
            Genres = new List<Genre>();
        }
    }
}
