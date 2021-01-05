using AppRestaurants.Data.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AppRestaurants.Data.Tests {
    [TestClass]
    public class DbTest {

        [TestMethod]
        public void CreateDbTest() {
            var optionsBuilder = new DbContextOptionsBuilder().UseSqlServer(@"server=.\SQLEXPRESS;database=B3Restaurants;trusted_connection=true;");
            using(var db = new RestaurantsContext(optionsBuilder.Options)) {
                db.Database.EnsureCreated();
            }
        }
    }
}
