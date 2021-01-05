using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppRestaurants.Data.Db;
using AppRestaurants.Data.Models;
using AppRestaurants.Services;

namespace AppRestaurants.Web.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly RestaurantsContext _context;
        private readonly IRestaurantsService _srv;

        public RestaurantsController(RestaurantsContext context, IRestaurantsService srv)
        {
            _context = context;
            _srv = srv;
        }

        // GET: RestaurantsController
        public ActionResult Home() {
            var liste = _srv.GetTopFiveWithGrades();
            return View(liste);
        }

        // GET: RestaurantsController/Index
        public async Task<IActionResult> Index()
        {
            var restaurantsContext = _context.Restaurants.Include(r => r.Adresse);
            return View(await restaurantsContext.ToListAsync());
        }

        // GET: Restaurants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants
                .Include(r => r.Adresse)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // GET: Restaurants/Create
        public IActionResult Create()
        {
            ViewData["AdresseID"] = new SelectList(_context.Set<Adresse>(), "ID", "ID");
            return View();
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nom,Telephone,Email,Details,AdresseID")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(restaurant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdresseID"] = new SelectList(_context.Set<Adresse>(), "ID", "ID", restaurant.AdresseID);
            return View(restaurant);
        }

        // GET: Restaurants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            ViewData["AdresseID"] = new SelectList(_context.Set<Adresse>(), "ID", "ID", restaurant.AdresseID);
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nom,Telephone,Email,Details,AdresseID")] Restaurant restaurant)
        {
            if (id != restaurant.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restaurant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantExists(restaurant.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdresseID"] = new SelectList(_context.Set<Adresse>(), "ID", "ID", restaurant.AdresseID);
            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants
                .Include(r => r.Adresse)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantExists(int id)
        {
            return _context.Restaurants.Any(e => e.ID == id);
        }
    }
}
