using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace AppRestaurants.Data.Models {
    public class Grade {

        [JsonIgnore]
        public int ID { get; set; }

        [Required]
        [Range(0, 10)]
        public int Note { get; set; }

        [Range(typeof(DateTime), "1/1/2000", "1/1/2100")]
        public DateTime DateDerniereVisite { get; set; }

        // TODO contrainte 255 caractères
        public string Commentaire { get; set; }
        public int RestaurantID { get; set; }

        [JsonIgnore]
        public Restaurant Restaurant { get; set; }

    }
}
