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
    public class GradesController : Controller
    {
        private readonly IRestaurantsService _restaurantsService;

        public GradesController(IRestaurantsService restaurantsService)
        {
            _restaurantsService = restaurantsService;
        }

        // GET: Grades
        //public async Task<IActionResult> Index()
        //{
        //    var restaurantsContext = _context.Grades.Include(g => g.Restaurant);
        //    return View(await restaurantsContext.ToListAsync());
        //}

        //// GET: Grades/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var grade = await _context.Grades
        //        .Include(g => g.Restaurant)
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (grade == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(grade);
        //}

        // GET: Grades/Create
        public IActionResult Create()
        {
            ViewData["Restaurants"] = new SelectList(_restaurantsService.GetRestaurantsList(), "ID", "Nom");
            return View();
        }

        // POST: Grades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Note,DateDerniereVisite,Commentaire,RestaurantID")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                _restaurantsService.CreateGrade(grade);
                return RedirectToAction("Index", "Restaurants");
            }
            ViewData["Restaurants"] = new SelectList(_restaurantsService.GetRestaurantsList(), "ID", "ID", grade.RestaurantID);
            return View(grade);
        }

        // GET: Grades/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var grade = await _context.Grades.FindAsync(id);
        //    if (grade == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["RestaurantID"] = new SelectList(_context.Restaurants, "ID", "ID", grade.RestaurantID);
        //    return View(grade);
        //}

        //// POST: Grades/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ID,Note,DateDerniereVisite,Commentaire,RestaurantID")] Grade grade)
        //{
        //    if (id != grade.ID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(grade);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!GradeExists(grade.ID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["RestaurantID"] = new SelectList(_context.Restaurants, "ID", "ID", grade.RestaurantID);
        //    return View(grade);
        //}

        //// GET: Grades/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var grade = await _context.Grades
        //        .Include(g => g.Restaurant)
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (grade == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(grade);
        //}

        //// POST: Grades/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var grade = await _context.Grades.FindAsync(id);
        //    _context.Grades.Remove(grade);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool GradeExists(int id)
        //{
        //    return _context.Grades.Any(e => e.ID == id);
        //}
    }
}
