using AppRestaurants.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

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
                foreach (var restau in listeRestaurants) {
                    string v = JsonConvert.SerializeObject(restau);
                    sw.WriteLine(v);
                }
            }
        }
    }
}
