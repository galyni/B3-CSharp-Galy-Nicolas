using System;
using System.Collections.Generic;
using System.Text;

namespace AppRestaurants.Data.Models {
    public class Adresse {
        public int ID { get; set; }
        public string Rue { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }

    }
}
