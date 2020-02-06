using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Movies.DB;
using PagedList;
using Movies.Data;
using Microsoft.AspNet.Identity;
using Movies.Web.Controllers;

namespace Movies.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdministrationController : Controller
    {
        private readonly MoviesRepository repo;
        private readonly RattingRepository rattingRepo;
        private readonly GenreRepository genreRepo;
        public AdministrationController()
        {
            this.repo = new MoviesRepository();
            this.rattingRepo = new RattingRepository();
            this.genreRepo = new GenreRepository();
        }

        [HttpPost]
        public ActionResult AddMovie(DB.Movies m)
        {
            if (!ModelState.IsValid)
            {
                PopuateSourceForDropdowns();

                return View("Create", m);
            }

            repo.AddMovie(m);

            return RedirectToAction("Index", "Administration");
        }

        [HttpGet]
        public ActionResult Create()
        {
            PopuateSourceForDropdowns();

            return this.View();
        }

        private void PopuateSourceForDropdowns()
        {
            
            var genreList = genreRepo.SelectAll();
            SelectList list2 = new SelectList(genreList, "Id", "Name");
            ViewBag.genreList = list2;

            var ratlist = rattingRepo.SelectAll();
            SelectList list = new SelectList(ratlist, "Id", "Ratting1");
            ViewBag.ratlistname = list;
        }

        [HttpGet]
        public ActionResult EditById(int id)
        {
            var movie = repo.GetById(id);

            var genreList = genreRepo.SelectAll();
            SelectList list2 = new SelectList(genreList, "Id", "Name");
            ViewBag.genreList = list2;

            var ratlist = rattingRepo.SelectAll();
            SelectList list = new SelectList(ratlist, "Id", "Ratting1");
            ViewBag.ratlistname = list;


            return View(movie);
        }

      


        [HttpPost]
        public ActionResult EditById(DB.Movies movie)
        {
            repo.UpdateMovie(movie);
            return RedirectToAction("Index", "Administration");
        }

        public ActionResult Delete(int id)
        {
            this.repo.DeleteMovie(id);
            return RedirectToAction("Index", "Administration");
        }

        // GET: Administration
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
    }
}