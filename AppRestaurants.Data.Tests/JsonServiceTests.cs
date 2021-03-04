using AppRestaurants.Data.Db;
using AppRestaurants.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace AppRestaurants.Data.Tests {




    [TestClass]
    public class JsonServiceTests {
        private string fileName = "../../../Ressources/restaurants.net.json";
        private string connectionString = @"server=.\SQLEXPRESS;database=B3Restaurants;trusted_connection=true;";
        private DbContextOptionsBuilder optionsBuilder;
        private JsonService _jsonService;

        [TestInitialize]
        public void Setup() {
            _jsonService = new JsonService();
            optionsBuilder = new DbContextOptionsBuilder().UseSqlServer(connectionString);
        }


        [TestMethod]
        public void SaveToFileTest() {
            using (var db = new RestaurantsContext(optionsBuilder.Options)) {
                try {
                    var restaurants = db.Restaurants.ToList();
                    _jsonService.SaveToFile(restaurants, fileName);
                    Assert.IsTrue(System.IO.File.Exists(fileName));
                } catch (Exception) {
                    Assert.Fail();
                }
            }
        }


        [TestMethod]
        public void BackupDatabaseToJsonTest() {
            try {
                _jsonService.BackupDatabaseToJson(fileName, connectionString);
            } catch (Exception) {
                Assert.Fail();
            }

        }

        [TestMethod]
        public void LoadFromFileTest() {
            using (var db = new RestaurantsContext(optionsBuilder.Options)) {
                try {
                    var restaurants = _jsonService.LoadFromFile(fileName);
                    Assert.AreEqual(restaurants.Count, db.Restaurants.Count());
                } catch (Exception) {
                    Assert.Fail();
                }
            }
        }

        [TestMethod]
        public void RestoreDatabaseFromJsonTest() {
            string connectionStringTest = @"server=.\SQLEXPRESS;database=B3Restaurants;trusted_connection=true;";
            try {
                _jsonService.RestoreDatabaseFromJson(fileName, connectionStringTest);
            } catch (Exception e) {
                Assert.Fail();
            }
        }

    }
}
