using AppRestaurants.Data.Db;
using AppRestaurants.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AppRestaurants.Services {
    public class RestaurantsService : IRestaurantsService {
        private RestaurantsContext _ctx;
        public RestaurantsService(RestaurantsContext ctx) {
            _ctx = ctx;
        }
        // TODO : revoir : IQueryable ou List ?
        public virtual List<Restaurant> GetRestaurantsList() {
            return _ctx.Restaurants.ToList();
        }
        public virtual List<Restaurant> GetRestaurantsListWithRelations() {
            return _ctx.Restaurants.Include(r => r.Adresse).Include(r => r.LastGrade).ToList();
        }

        public virtual List<Restaurant> GetTopFiveWithGrades() {
            return _ctx.Restaurants.Include(r => r.LastGrade).OrderByDescending(r => r.LastGrade.Note).Take(5).ToList();
        }

        public virtual Restaurant GetRestaurantById(int id) {
            return _ctx.Restaurants.Find(id);

        }
        public virtual Restaurant GetRestaurantWithAdresse(int id) {
            return _ctx.Restaurants.Include(r => r.Adresse).FirstOrDefault(r => r.ID == id);
        }

        public virtual void CreateRestaurant(Restaurant restaurant) {
            _ctx.Restaurants.Add(restaurant);
            _ctx.SaveChanges();
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
            var restaurant = GetRestaurantWithAdresse(id);
            _ctx.Remove(restaurant);
            _ctx.Remove(restaurant.Adresse);
            _ctx.SaveChanges();
        }

        //private bool RestaurantExists(int id) {
        //    return _ctx.Restaurants.Any(e => e.ID == id);
        //}
    }
}
