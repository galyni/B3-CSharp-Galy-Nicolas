using AppRestaurants.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppRestaurants.Web.Models {
    public class RestaurantViewModel {
        public string Nom { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Details { get; set; }
        public string Numero { get; set; }
        public string Rue { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
    }
}
