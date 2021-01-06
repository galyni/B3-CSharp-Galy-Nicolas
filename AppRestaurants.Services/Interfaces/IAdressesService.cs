using AppRestaurants.Data.Models;
using System.Collections.Generic;

namespace AppRestaurants.Services {
    public interface IAdressesService {
        void CreateAdresse(Adresse Adresse);
        void DeleteAdresse(int id);
        Adresse GetAdresseById(int id);
        List<Adresse> GetAdressesList();
        void UpdateAdresse(Adresse Adresse);
    }
}