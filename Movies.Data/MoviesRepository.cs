using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies.DB;
using Movies.BusinessLogic;

namespace Movies.Data
{
    public class MoviesRepository:IMoviesRepository
    {
        MovieFPEntities context = new MovieFPEntities();

        public List<DB.Movies> SelectAll()
        {

            var list = context.Movies.OrderBy(x => x.Name).ToList();


            return list;
        }


        public List<DB.Movies> GetByGenre(int id)
        {
            var list = context.Movies.Where(x => x.RattingId == id).ToList();
            return list;
        }

        public DB.Movies GetById(int id)
        {
            return context.Movies.SingleOrDefault(x => x.Id == id);
        }

        public void AddMovie(DB.Movies m)
        {
            var id = context.Movies.Max(x => x.Id);
            m.Id = id + 1;
            context.Movies.Add(m);
            context.SaveChanges();
        }

        public void UpdateMovie(DB.Movies m)
        {
            var movie = GetById(m.Id);
            if (movie != null)
            {
                movie.Name = m.Name;
                movie.RattingId = m.RattingId;
                movie.Year = m.Year;
                movie.GenreId = m.GenreId;
                context.SaveChanges();
            }

        }
        public void DeleteMovie(int id)
        {
            context.Movies.Remove(context.Movies.SingleOrDefault(x => x.Id == id));
            context.SaveChanges();
        }

    }
}
