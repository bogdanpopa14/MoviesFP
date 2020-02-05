using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Movies.Data;
using Movies.DB;
using PagedList;
using Microsoft.AspNet.Identity;


namespace Movies.Web.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        private readonly FavoritesRepository favRepo;
        private readonly RattingRepository rattRepo;

        public FavoritesController()
        {
            this.rattRepo = new RattingRepository();
            this.favRepo = new FavoritesRepository();
        }

        // GET: Favorites
        public ActionResult Index(int? page)
        {
            var list = favRepo.GetAll();
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            ViewBag.favList = favRepo.GetFavAll();
            var userId = User.Identity.GetUserId();
            var sm = favRepo.GetFavAllByUser(userId);

            return View(sm.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Delete(int id,string userId)
        {
            favRepo.Delete(id,userId);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Ratting(int id,string userId)
        {
            var f = favRepo.GetByIdAndUser(id,userId);
            var ratlist = rattRepo.SelectAll();
            SelectList list = new SelectList(ratlist, "Id", "Ratting1");
            ViewBag.ratlistname = list;
            return View(f);
        }

        [HttpPost]
        public ActionResult Ratting(Favorites f)
        {
            favRepo.EditRatting(f);
            return RedirectToAction("Index");
        }
    }
}