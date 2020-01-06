using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace Consol
{
    class Program
    {
        static void Main(string[] args)
        {
            using (AudioLibraryContext db = new AudioLibraryContext())
            {
                foreach (var genre in db.Genres)
                    Console.WriteLine(genre.Name);

                Console.WriteLine("Finished");
                Console.ReadLine();
            }
        }
    }
}
