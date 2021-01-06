using AppRestaurants.Data.Models;
using AppRestaurants.Services;
using AppRestaurants.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AppRestaurants.Web.Controllers {
    public class RestaurantsController : Controller {
        private readonly IRestaurantsService _restaurantsService;
        private readonly IAdressesService _adressesService;

        public RestaurantsController(IRestaurantsService restaurantsService, IAdressesService adressesService) {
            _restaurantsService = restaurantsService;
            _adressesService = adressesService;
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

        //// GET: Restaurants/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var restaurant = await _context.Restaurants
        //        .Include(r => r.Adresse)
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (restaurant == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(restaurant);
        //}

        // GET: Restaurants/Create
        public IActionResult Create() {
            RestaurantViewModel viewModel = new RestaurantViewModel();
            return View(viewModel);
        }

        // POST: Restaurants/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //TODO : faire un choix cohérent : service adresse ou non ?
        public IActionResult Create([Bind("Nom,Telephone,Email,Details")] Restaurant restaurant, [Bind("Numero, Rue, Ville, CodePostal")] Adresse adresse) {
            if (ModelState.IsValid) {
                _adressesService.CreateAdresse(adresse);
                restaurant.Adresse = adresse;
                _restaurantsService.CreateRestaurant(restaurant);
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

            //RestaurantViewModel viewModel = new RestaurantViewModel {
            //    Nom = restaurant.Nom,
            //    Telephone = restaurant.Telephone,
            //    Email = restaurant.Email,
            //    Details = restaurant.Details,
            //    Numero = restaurant.Adresse.Numero,
            //    Rue = restaurant.Adresse.Rue,
            //    CodePostal = restaurant.Adresse.CodePostal,
            //    Ville = restaurant.Adresse.Ville
            //};

            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID, Nom,Telephone,Email,Details, AdresseID")] Restaurant restaurant) {
            if (id != restaurant.ID) {
                return NotFound();
            }
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
        _restaurantsService.DeleteRestaurant(id);
        return RedirectToAction(nameof(Index));
    }


}
}
