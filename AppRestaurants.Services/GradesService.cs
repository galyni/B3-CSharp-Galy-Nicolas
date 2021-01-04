using AppRestaurants.Data.Db;
using AppRestaurants.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRestaurants.Services {
    class GradesService {
        private RestaurantsContext _ctx;
        public GradesService(RestaurantsContext ctx) {
            _ctx = ctx;
        }

       
    }
}
