using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AppRestaurants.Data.Models {
    public class Restaurant {

        /* La sérialisation des ID posait le problème de leurs insertion dans une nouvelle base. 2 options : activer IDENTITY_INSERT sur les tables, ou renoncer aux ID
         Si l'on voulait s'assurer de ne pas perdre la correspondance des ID entre deux tables après sérialisation / désérialisation, il faudrait choisir la deuxième option.
         Ici, l'enjeu était simplement de pouvoir retrouver les tables avec leurs relations.
         */
        [JsonIgnore]        
        public int ID { get; set; }
        [Required]
        public string Nom { get; set; }

        [Phone]
        public string Telephone { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Details { get; set; }
        public int AdresseID { get; set; }
        public Adresse Adresse { get; set; }
        public Grade LastGrade { get; set; }
    }
}
