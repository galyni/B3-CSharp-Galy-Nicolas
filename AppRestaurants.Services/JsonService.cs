using AppRestaurants.Data.Db;
using AppRestaurants.Data.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AppRestaurants.Services {
    public class JsonService {
        public List<Restaurant> LoadFromFile(string fileName) {
            using (var sr = new StreamReader(fileName)) {
                string fileContent = sr.ReadToEnd();
                var result = JsonConvert.DeserializeObject<List<Restaurant>>(fileContent);
                return result;
            }
        }
        public void SaveToFile(List<Restaurant> listeRestaurants, string fileName) {
            using (var sw = new StreamWriter(fileName)) {
                    var v = JsonConvert.SerializeObject(listeRestaurants);
                    sw.WriteLine(v);
            }
        }

        public void RestoreDatabaseFromJson(string fileName, string connectionString) {
            var restaurants = LoadFromFile(fileName);
            using (var db = new RestaurantsContext((new DbContextOptionsBuilder().UseSqlServer(connectionString)).Options)) {
                db.Database.EnsureCreated();
                restaurants.ForEach(r => {
                    db.Add(r);
                });
                db.SaveChanges();
            }
        }

        public void BackupDatabaseToJson(string fileName, string connectionString) {
            using (var db = new RestaurantsContext((new DbContextOptionsBuilder().UseSqlServer(connectionString)).Options)) {
                db.Database.EnsureCreated();
                var restaurants = db.Restaurants.Include(r => r.Adresse).Include(r => r.LastGrade).ToList();
                SaveToFile(restaurants, fileName);
            }
        }
    }
}
