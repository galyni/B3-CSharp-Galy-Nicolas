using AppRestaurants.Data.Db;
using AppRestaurants.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRestaurants.Services {
    public class GradesService {
        private RestaurantsContext _ctx;
        public GradesService(RestaurantsContext ctx) {
            _ctx = ctx;
        }
        public virtual void Create(Grade grade) {
            _ctx.Grades.Add(grade);
            _ctx.SaveChanges();
        }
    }
}
