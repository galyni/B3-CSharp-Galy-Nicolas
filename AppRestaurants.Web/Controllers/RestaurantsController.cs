using AppRestaurants.Data.Models;
using AppRestaurants.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace AppRestaurants.Web.Controllers {
    public class RestaurantsController : Controller {
        private readonly IRestaurantsService _restaurantsService;

        public RestaurantsController(IRestaurantsService restaurantsService) {
            _restaurantsService = restaurantsService;
        }

        // GET: RestaurantsController
        public IActionResult Home() {
            try {
                var liste = _restaurantsService.GetTopFiveWithGrades();
                return View(liste);
            } catch {
                return NotFound();
            }
        }

        // GET: RestaurantsController/Index
        public IActionResult Index() {
            List<Restaurant> liste;
            try {
                liste = _restaurantsService.GetRestaurantsListWithRelations();
            } catch {
                return NotFound();
            }
            return View(liste);
        }

        // GET: Restaurants/Details/5
        public IActionResult Details(int? id) {
            if (id == null) {
                return NotFound();
            }
            try {
                var restaurant = _restaurantsService.GetRestaurantWithRelations((int)id);
                if (restaurant == null) {
                    return NotFound();
                }
                return View(restaurant);
            } catch {
                return NotFound();
            }
        }

        // GET: Restaurants/Create
        public IActionResult CreateRestaurant() {
            Restaurant restaurant = new Restaurant();
            return View(restaurant);
        }

        // POST: Restaurants/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateRestaurant(Restaurant restaurant) {
            if (ModelState.IsValid) {
                //restaurant.Adresse = adresse;
                try {
                    _restaurantsService.CreateRestaurant(restaurant);
                } catch (Exception) {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(restaurant);
        }

        // GET: Restaurants/Edit/5
        public IActionResult Edit(int? id) {
            if (id == null) {
                return NotFound();
            }
            try {
                var restaurant = _restaurantsService.GetRestaurantWithAdresse((int)id);

                if (restaurant == null) {
                    return NotFound();
                }
                return View(restaurant);
            } catch {
                return NotFound();
            }

        }

        // POST: Restaurants/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Restaurant restaurant) {
            //if (id != restaurant.ID) {
            //return NotFound();
            //}
            if (ModelState.IsValid) {
                try {
                    _restaurantsService.UpdateRestaurant(restaurant);
                } catch (Exception) {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        public IActionResult Delete(int? id) {
            if (id == null) {
                return NotFound();
            }
            try {
                var restaurant = _restaurantsService.GetRestaurantById((int)id);
                if (restaurant == null) {
                    return NotFound();
                }
                return View(restaurant);
            } catch (Exception) {
                return NotFound();
            }
        }


        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id) {
            try {
                _restaurantsService.DeleteRestaurant(id);
            } catch {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }


        public IActionResult CreateGrade() {
            ViewData["Restaurants"] = new SelectList(_restaurantsService.GetRestaurantsList(), "ID", "Nom");
            return View();
        }

        // POST: Grades/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateGrade(Grade grade) {
            if (ModelState.IsValid) {
                try {
                    _restaurantsService.GradeRestaurant(grade);
                    return RedirectToAction(nameof(GradesList));
                } catch {
                    return NotFound();
                }
            }
            ViewData["Restaurants"] = new SelectList(_restaurantsService.GetRestaurantsList(), "ID", "ID", grade.RestaurantID);
            return View(grade);
        }

        public IActionResult CreateGradeForRestaurant(int id) {
            var restaurant = _restaurantsService.GetRestaurantById(id);
            //var grade = new Grade() { Restaurant = restaurant };
            return View(restaurant);
        }

        // POST: Grades/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateGradeForRestaurant(Restaurant restaurant) {
            if (restaurant.LastGrade != null) {
                try {
                    restaurant.LastGrade.RestaurantID = restaurant.ID;
                    _restaurantsService.GradeRestaurant(restaurant.LastGrade);
                    return RedirectToAction(nameof(GradesList));
                } catch {
                    return NotFound();
                }
            }
            return View(restaurant);
        }

        public IActionResult GradesList() {
            try {
                var restaurants = _restaurantsService.GetRestaurantsListWithGrades();
                return View(restaurants);
            } catch {
                return NotFound();
            }
        }

    }
}
