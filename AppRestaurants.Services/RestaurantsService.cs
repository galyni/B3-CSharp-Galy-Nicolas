using AppRestaurants.Data.Db;
using AppRestaurants.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRestaurants.Services {
    class RestaurantsService {
        private RestaurantsContext _ctx;
        public RestaurantsService(RestaurantsContext ctx) {
            _ctx = ctx;
        }

        public virtual List<Restaurant> GetRestaurantsList() {
            return _ctx.Restaurants.ToList();
        }
        
        public virtual Restaurant GetRestaurantById(int id) {
            return _ctx.Restaurants.Find(id);

        }
        
        public virtual void CreateRestaurant(Restaurant restaurant) {
            _ctx.Restaurants.Add(restaurant);
            _ctx.SaveChanges();
        }
        public virtual void UpdateRestaurant(Restaurant restaurant) {
            _ctx.Restaurants.Update(restaurant);
            _ctx.SaveChanges();
        }
        public void DeleteRestaurant(int id) {
            var restaurant = _ctx.Restaurants.Find(id);
            _ctx.Remove(restaurant);
            _ctx.SaveChanges();
        }
    }
}
