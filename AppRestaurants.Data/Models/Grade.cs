using Newtonsoft.Json;
using System;

namespace AppRestaurants.Data.Models {
    public class Grade {
        [JsonIgnore]
        public int ID { get; set; }
        public int Note { get; set; }
        public DateTime DateDerniereVisite { get; set; }

        // TODO contrainte 255 caractères
        public string Commentaire { get; set; }
        public int RestaurantID { get; set; }
        [JsonIgnore]
        public Restaurant Restaurant { get; set; }

    }
}
