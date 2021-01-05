using System;
using System.Collections.Generic;
using System.Text;

namespace AppRestaurants.Data.Models {
    public class Grade {
        public int ID { get; set; }
        public int Note { get; set; }
        public DateTime DateDerniereVisite { get; set; }

        // TODO contrainte 255 caractères
        public string Commentaire { get; set; }
        public int RestaurantID { get; set; }
        public Restaurant Restaurant { get; set; }

    }
}
