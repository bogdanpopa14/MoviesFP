using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies.DB;

namespace Movies.Data
{
    public class FavoritesRepository
    {

        MovieFPEntities context = new MovieFPEntities();
        public void AddToFav(int id,string userId)
        {
            var curentIndex = 0;
            var idList = context.Favorites.Select(x => x.Id).ToList();
            if (idList.Count == 0)
            {
                curentIndex = 0;
            }
            else
            {
                curentIndex = idList[idList.Count - 1];
            }

            var movieContains = context.Favorites.Select(x => x.MovieId).ToList();
            if (!movieContains.Contains(id))
            {
                var fav = new Favorites();
                fav.Id = curentIndex + 1;
                curentIndex++;
                fav.MovieId = id;
                fav.UserId = userId;
                context.Favorites.Add(fav);
            }



            context.SaveChanges();
        }

        public List<Favorites> GetFavAllByUser(string userId)
        {
            return context.Favorites.Where(x => x.UserId == userId).Select(x => x).ToList();
        }

        public List<Favorites> GetFavAll()
        {
            return context.Favorites.ToList();
        }
        public Favorites GetByIdAndUser(int id,string userId)
        {
            return context.Favorites.SingleOrDefault(x => x.MovieId == id && x.UserId == userId);
        }
        public Favorites GetById(int id)
        {
            return context.Favorites.SingleOrDefault(x => x.MovieId == id);
        }

        public void EditRatting(Favorites f)
        {
            var fav = GetByIdAndUser(f.Id,f.UserId);
            fav.RattingId = f.RattingId;
            context.SaveChanges();
        }

        public void Delete(int id,string userId)
        {
            context.Favorites.Remove(context.Favorites.SingleOrDefault(x => x.MovieId == id && x.UserId==userId));
            context.SaveChanges();
        }

        public List<DB.Movies> GetAll()
        {

            List<DB.Movies> list = new List<DB.Movies>();
            var fav = context.Favorites.Select(x => x.MovieId).ToList();
            for (int i = 0; i < fav.Count; i++)
            {
                var comp = fav[i];

                list.AddRange(context.Movies.Where(x => x.Id == comp).Select(x => x).ToList());
            }
            return list;
        }
    }
}
