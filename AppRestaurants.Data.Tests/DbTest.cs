using AppRestaurants.Data.Db;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppRestaurants.Data.Tests {
    [TestClass]
    public class DbTest {
        [TestMethod]
        public void CreateDbTest() {
            using(var db = new RestaurantsContext()) {
                db.Database.EnsureCreated();
            }
        }
    }
}
