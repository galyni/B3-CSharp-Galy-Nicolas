using AppRestaurants.Data.Db;
using AppRestaurants.Services;
using AppRestaurants.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppRestaurants.Web.Controllers {
    public class RestaurantsController : Controller {
        private IRestaurantsService _srv;

        public RestaurantsController(IRestaurantsService srv) {
            _srv = srv;
        }

        // GET: RestaurantsController
        public ActionResult Home() {
            var liste = _srv.GetTopFiveWithGrades();
            return View(liste);
        }

        // GET: RestaurantsController/Index
        public ActionResult Index() {
            var liste = _srv.GetRestaurantsList();
            var model = new RestaurantsIndexViewModel() { Restaurants = liste };
            return View(liste);
        }

        // GET: RestaurantsController/Details/5
        public ActionResult Details(int id) {
            return View();
        }

        // GET: RestaurantsController/Create
        public ActionResult Create() {
            return View();
        }

        // POST: RestaurantsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }

        // GET: RestaurantsController/Edit/5
        public ActionResult Edit(int id) {
            return View();
        }

        // POST: RestaurantsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }

        // GET: RestaurantsController/Delete/5
        public ActionResult Delete(int id) {
            return View();
        }

        // POST: RestaurantsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }
    }
}
