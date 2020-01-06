using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CommentaryDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DateOfCreation { get; set; }
        public UserDTO User { get; set; }
        public int SongId { get; set; }
    }
}
