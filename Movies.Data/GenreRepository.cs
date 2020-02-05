using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies.DB;

namespace Movies.Data
{
    public class GenreRepository
    {
        MovieFPEntities context = new MovieFPEntities();
        public List<Genre> SelectAll()
        {
            return context.Genre.OrderBy(x => x.Name).ToList();
        }

        public Genre GetById(int id)
        {
            return context.Genre.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
