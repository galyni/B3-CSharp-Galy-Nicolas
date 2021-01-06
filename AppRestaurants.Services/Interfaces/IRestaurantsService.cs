using AppRestaurants.Data.Models;
using System.Collections.Generic;

namespace AppRestaurants.Services {
    public interface IRestaurantsService {
        void CreateRestaurant(Restaurant restaurant);
        void DeleteRestaurant(int id);
        Restaurant GetRestaurantById(int id);
        Restaurant GetRestaurantWithAdresse(int id);
        List<Restaurant> GetRestaurantsList();
        List<Restaurant> GetRestaurantsListWithRelations();
        List<Restaurant> GetTopFiveWithGrades();
        void UpdateRestaurant(Restaurant restaurant);
    }
}