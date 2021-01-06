using AppRestaurants.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppRestaurants.Data.Db {
   public  class RestaurantsContext : DbContext {

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Adresse> Adresses { get; set; }

        public RestaurantsContext(DbContextOptions options) : base(options) {

        }

    }
}
