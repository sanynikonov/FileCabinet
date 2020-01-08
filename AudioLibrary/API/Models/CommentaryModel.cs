using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API
{
    public class CommentaryModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DateOfCreation { get; set; }
        public UserModel User { get; set; }
        public int SongId { get; set; }
    }
}