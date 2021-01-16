using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppRestaurants.Data.Models {
    public class Adresse {

        [JsonIgnore]
        public int ID { get; set; }
        public string Numero { get; set; }
        public string Rue { get; set; }

        
        [RegularExpression("[0-9]{5}", ErrorMessage ="Ce n'est pas un code postal valide")]
        public string CodePostal { get; set; }

        [Required]
        public string Ville { get; set; }

        public override string ToString() {
            return string.Join(" ", Numero, Rue, CodePostal, Ville);
        }

    }
}
