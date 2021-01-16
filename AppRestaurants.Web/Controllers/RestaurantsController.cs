using AppRestaurants.Data.Models;
using AppRestaurants.Services;
using AppRestaurants.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace AppRestaurants.Web.Controllers {
    public class RestaurantsController : Controller {
        private readonly IRestaurantsService _restaurantsService;

        public RestaurantsController(IRestaurantsService restaurantsService) {
            _restaurantsService = restaurantsService;
        }

        // GET: RestaurantsController
        public IActionResult Home() {
            var liste = _restaurantsService.GetTopFiveWithGrades();
            return View(liste);
        }

        // GET: RestaurantsController/Index
        public IActionResult Index() {
            var liste = _restaurantsService.GetRestaurantsListWithRelations();
            return View(liste);
        }

        // GET: Restaurants/Details/5
        public  IActionResult Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var restaurant = _restaurantsService.GetRestaurantWithRelations((int)id);
            if (restaurant == null) {
                return NotFound();
            }

            return View(restaurant);
        }

        // GET: Restaurants/Create
        public IActionResult Create() {
            Restaurant restaurant = new Restaurant();
            return View(restaurant);
        }

        // POST: Restaurants/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //TODO : faire un choix cohérent : service adresse ou non ?
        public IActionResult Create([Bind("Nom,Telephone,Email,Details")] Restaurant restaurant, [Bind("Numero, Rue, Ville, CodePostal")] Adresse adresse) {
            if (ModelState.IsValid) {
                restaurant.Adresse = adresse;
                try {
                    _restaurantsService.CreateRestaurant(restaurant);
                } catch(Exception) {
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
            // TODO : édition adresse
            var restaurant = _restaurantsService.GetRestaurantWithAdresse((int)id);
            if (restaurant == null) {
                return NotFound();
            }

            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID, Nom,Telephone,Email,Details, AdresseID")] Restaurant restaurant, [Bind("AdresseID, Numero, Rue, Ville, CodePostal")] Adresse adresse) {
            if (id != restaurant.ID) {
                return NotFound();
            }
            if (ModelState.IsValid) {
                try {
                    restaurant.Adresse = adresse;
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
            var restaurant = _restaurantsService.GetRestaurantById((int)id);
            if (restaurant == null) {
                return NotFound();
            }

            return View(restaurant);
        }


        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id) {
            // TODO : faire ça ailleurs
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateGrade([Bind("Note, DateDerniereVisite, Commentaire, RestaurantID")] Grade grade) {
            if (ModelState.IsValid) {
                _restaurantsService.GradeRestaurant(grade);
                return RedirectToAction(nameof(GradesList));
            }
            ViewData["Restaurants"] = new SelectList(_restaurantsService.GetRestaurantsList(), "ID", "ID", grade.RestaurantID);
            return View(grade);
        }

        public IActionResult CreateGradeForRestaurant(int id) {
            var restaurant = _restaurantsService.GetRestaurantById(id);
            var grade = new Grade() { Restaurant = restaurant, RestaurantID = restaurant.ID };
            return View(grade);
        }

        // POST: Grades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateGradeForRestaurant([Bind("Note, DateDerniereVisite, Commentaire, RestaurantID")] Grade grade) {
            if (ModelState.IsValid) {
                _restaurantsService.GradeRestaurant(grade);
                return RedirectToAction(nameof(GradesList));
            }
            return View(grade);
        }

        public IActionResult GradesList() {
            var restaurants = _restaurantsService.GetRestaurantsListWithGrades();
            return View(restaurants);
        }

    }
}
