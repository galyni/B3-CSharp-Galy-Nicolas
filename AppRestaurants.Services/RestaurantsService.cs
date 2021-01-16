using AppRestaurants.Data.Db;
using AppRestaurants.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppRestaurants.Services {
    public class RestaurantsService : IRestaurantsService {
        private RestaurantsContext _ctx;

        public RestaurantsService(RestaurantsContext ctx) {
            _ctx = ctx;
        }
        // TODO : revoir : IQueryable ou List ?
        // TODO : refactoriser les méthodes, ou mieux organiser.
        // TODO : supprimer les méthodes non utilisées
        public virtual List<Restaurant> GetRestaurantsList() {
            return _ctx.Restaurants.ToList();
        }

        public virtual List<Restaurant> GetRestaurantsListWithGrades() {
            return _ctx.Restaurants.Include(r => r.LastGrade).ToList();
        }

        public virtual List<Restaurant> GetRestaurantsListWithRelations() {
            return _ctx.Restaurants.Include(r => r.Adresse).Include(r => r.LastGrade).ToList();
        }


        public virtual List<Restaurant> GetTopFiveWithGrades() {
            return _ctx.Restaurants.Include(r => r.LastGrade).Include(r => r.Adresse).Where(r => r.Adresse.Ville.ToLower() == "grenoble").OrderByDescending(r => r.LastGrade.Note).Take(5).ToList();
        }

        public virtual Restaurant GetRestaurantById(int id) {
            return _ctx.Restaurants.Find(id);

        }
        public virtual Restaurant GetRestaurantWithAdresse(int id) {
            return _ctx.Restaurants.Include(r => r.Adresse).FirstOrDefault(r => r.ID == id);
        }



        public virtual Restaurant GetRestaurantWithRelations(int id) {
            return _ctx.Restaurants.Include(r => r.Adresse).Include(r => r.LastGrade).FirstOrDefault(r => r.ID == id);
        }

        public virtual void CreateRestaurant(Restaurant restaurant) {
            _ctx.Restaurants.Add(restaurant);
            _ctx.SaveChanges();
        }

        public virtual void GradeRestaurant(Grade grade) {
            // On ne sauvegarde que la dernière note.
            if (RestaurantHasGrade(grade.RestaurantID)) {
                grade.ID = _ctx.Grades.Where(g => g.RestaurantID == grade.RestaurantID).Select(g => g.ID).FirstOrDefault();
                _ctx.Grades.Update(grade);
            } else {
                _ctx.Grades.Add(grade);
            }
            _ctx.SaveChanges();
        }

        private bool RestaurantHasGrade(int restaurantID) {
            return _ctx.Grades.Any(r => r.RestaurantID == restaurantID);
        }

        public virtual void UpdateRestaurant(Restaurant restaurant) {
            try {
                _ctx.Restaurants.Update(restaurant);
            } catch (DbUpdateConcurrencyException) {
                // TODO : améliorer la gestion d'exceptions
                //if (!RestaurantExists(restaurant.ID)) {
                //    throw;
                //} else {
                throw;
            }
            _ctx.SaveChanges();
        }

        public void DeleteRestaurant(int id) {
            // On supprime l'adresse associée au restaurant, parce qu'on suppose qu'il n'y a qu'un seul restaurant par adresse et qu'on ne manipule jamais les adresses elles-mêmes
            // Pour la note, on la supprime avec le restaurant
            var restaurant = GetRestaurantWithRelations(id);
            _ctx.Remove(restaurant);
            _ctx.Remove(restaurant.Adresse);
            if(restaurant.LastGrade != null)
                _ctx.Remove(restaurant.LastGrade);
            _ctx.SaveChanges();
        }

        //private bool RestaurantExists(int id) {
        //    return _ctx.Restaurants.Any(e => e.ID == id);
        //}
    }
}
