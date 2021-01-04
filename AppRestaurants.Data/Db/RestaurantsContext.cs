﻿using AppRestaurants.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppRestaurants.Data.Db {
   public  class RestaurantsContext : DbContext {

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            // c'est une mauvaise pratique que nous corrigerons ultérieurement
            optionsBuilder.UseSqlServer(
               @"server=.\SQLEXPRESS;database=B3Restaurants;trusted_connection=true;");
        }
    }
}
