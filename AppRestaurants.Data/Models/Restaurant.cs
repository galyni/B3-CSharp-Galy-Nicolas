using System;
using System.Collections.Generic;
using System.Text;

namespace AppRestaurants.Data.Models {
    class Restaurant {
        public int ID { get; set; }
        public string Nom { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Commentaire { get; set; }
        public Adresse Adresse { get; set; }
        public Grade LastGrade { get; set; }

    }
}
