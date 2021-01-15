﻿using AppRestaurants.Data.Models;
using System.Collections.Generic;

namespace AppRestaurants.Services {
    public interface IRestaurantsService {
        void CreateRestaurant(Restaurant restaurant);
        void CreateGrade(Grade grade);
        void DeleteRestaurant(int id);
        Restaurant GetRestaurantById(int id);
        Restaurant GetRestaurantWithAdresse(int id);
        Restaurant GetRestaurantWithRelations(int id);
        List<Restaurant> GetRestaurantsListWithGrades();
        List<Restaurant> GetRestaurantsList();
        List<Restaurant> GetRestaurantsListWithRelations();
        List<Restaurant> GetTopFiveWithGrades();
        void UpdateRestaurant(Restaurant restaurant);
    }
}