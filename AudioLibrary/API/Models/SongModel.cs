using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API
{
    public class SongModel
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        public int Listens { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string AudioPath { get; set; }
        [Required]
        public int AlbumId { get; set; }
        public int LikesAmount { get; set; }
        public bool LikedByCurrentUser { get; set; }
    }
}