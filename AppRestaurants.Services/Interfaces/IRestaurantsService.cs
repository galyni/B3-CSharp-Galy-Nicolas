using AppRestaurants.Data.Models;
using System.Collections.Generic;

namespace AppRestaurants.Services {
    public interface IRestaurantsService {
        void CreateRestaurant(Restaurant restaurant);
        void DeleteRestaurant(int id);
        Restaurant GetRestaurantById(int id);
        List<Restaurant> GetRestaurantsList();
        List<Restaurant> GetTopFiveWithGrades();
        void UpdateRestaurant(Restaurant restaurant);
    }
}