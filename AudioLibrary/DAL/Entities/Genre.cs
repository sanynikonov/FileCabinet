using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        public string Name{ get; set; }

        public virtual List<SongsContainer> SongsContainers { get; set; }

        public Genre()
        {
            SongsContainers = new List<SongsContainer>();
        }
    }
}
