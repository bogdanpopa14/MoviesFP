using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Movies.Data;
using Movies.DB;
using PagedList;

namespace Movies.Web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MoviesRepository repo;
        private readonly FavoritesRepository favRepo;
        private readonly GenreRepository genreRepo;
        public MoviesController()
        {

            this.repo = new MoviesRepository();
            this.genreRepo = new GenreRepository();
            this.favRepo = new FavoritesRepository();
        }
        // GET: Movies
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.YearSortParm = sortOrder == "Year" ? "year_desc" : "Year";
            ViewBag.RattingSortParam = sortOrder == "Ratting" ? "ratting_desc" : "Ratting";


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var list = repo.SelectAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                //list = list.Where(x => x.Name.Contains(searchString)).ToList();
                list = list.Where(x => x.Name.IndexOf(searchString, StringComparison.CurrentCultureIgnoreCase) != -1).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    list = list.OrderByDescending(x => x.Name).ToList();
                    break;
                case "Year":
                    list = list.OrderBy(x => x.Year).ToList();
                    break;
                case "year_desc":
                    list = list.OrderByDescending(x => x.Year).ToList();
                    break;
                case "Ratting":
                    list = list.OrderBy(x => x.Ratting.Ratting1).ToList();
                    break;
                case "ratting_desc":
                    list = list.OrderByDescending(x => x.Ratting.Ratting1).ToList();
                    break;
                default:
                    list = list.OrderBy(x => x.Name).ToList();
                    break;

            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ByGenre()
        {
            var list = genreRepo.SelectAll();
            return View(list);
        }

        public ActionResult MoviesByGenre(int id)
        {
            var genre = genreRepo.GetById(id);
            ViewBag.genre = genre.Name;
            var list = repo.GetByGenre(id);
            return View(list);
        }

        [Authorize]
        public ActionResult AddToFav(int id, string userId)
        {

            favRepo.AddToFav(id,userId);
            return RedirectToAction("Index");
        }
    }
}