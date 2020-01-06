using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Commentary
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DateOfCreation { get; set; }

        public virtual User User { get; set; }
        public virtual Song Song { get; set; }
    }
}
