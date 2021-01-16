using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppRestaurants.Data.Models {
    public class Adresse {
        [JsonIgnore]
        public int ID { get; set; }
        public string Numero { get; set; }
        public string Rue { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }

        public override string ToString() {
            return string.Join(" ", Numero, Rue, CodePostal, Ville);
        }

    }
}
