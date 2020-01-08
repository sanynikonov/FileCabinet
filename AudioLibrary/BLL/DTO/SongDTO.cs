using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SongDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Listens { get; set; }
        public string AudioPath { get; set; }
        public int AlbumId { get; set; }
        public int LikesAmount { get; set; }
        public bool LikedByCurrentUser { get; set; }
    }
}
