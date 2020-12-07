using System;
using System.Collections.Generic;
using System.Text;

namespace AppRestaurants.Data.Models {
    class Grade {
        public int Note { get; set; }
        public DateTime DateDerniereVisite { get; set; }

        // TODO contrainte 255 caractères
        public string Commentaire { get; set; }

    }
}
