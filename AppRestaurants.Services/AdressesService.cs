using AppRestaurants.Data.Db;
using AppRestaurants.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurants.Services {
    public class AdressesService : IAdressesService {
        private RestaurantsContext _ctx;
        public AdressesService(RestaurantsContext ctx) {
            _ctx = ctx;
        }
        public virtual List<Adresse> GetAdressesList() {
            return _ctx.Adresses.ToList();
        }
        public virtual Adresse GetAdresseById(int id) {
            return _ctx.Adresses.Find(id);

        }

        public virtual void CreateAdresse(Adresse adresse) {
           _ctx.Adresses.Add(adresse);
            _ctx.SaveChanges();
        }
        public virtual void UpdateAdresse(Adresse adresse) {
            _ctx.Adresses.Update(adresse);
            _ctx.SaveChanges();
        }
        public void DeleteAdresse(int id) {
            var adresse = _ctx.Adresses.Find(id);
            _ctx.Remove(adresse);
            _ctx.SaveChanges();
        }
    }
}
