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

        public DateTime DateDerniereVisite { get; set; }

        [StringLength(255)]
        public string Commentaire { get; set; }
        public int RestaurantID { get; set; }

        [JsonIgnore]
        public Restaurant Restaurant { get; set; }

    }
}
