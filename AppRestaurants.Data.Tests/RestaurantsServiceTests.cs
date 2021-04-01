using AppRestaurants.Data.Db;
using AppRestaurants.Data.Models;
using AppRestaurants.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace AppRestaurants.Data.Tests {

    // TODO : TestCleanup 
    [TestClass()]
    public class RestaurantsServiceTests {
        RestaurantsService srv;
        RestaurantsContext db;

        [TestInitialize()]
        public void Setup() {
            var optionsBuilder = new DbContextOptionsBuilder();
            // On se sert d'une base en mémoire, car on ne teste que le service. On la remplit avec un restaurant indépendamment du service.
            // J'ai l'impression que cela pose problème pour le tracking des entités : certains tests échouent ou non selon qu'on les lance indépendamment des autres ou tous ensemble...
            optionsBuilder.UseInMemoryDatabase("Restaurants");
            var options = optionsBuilder.Options;
            db = new RestaurantsContext(options);
            db.Restaurants.Add(new Restaurant() {
                Nom = "Test",
                Email = "test@test.fr",
                Telephone = "25411225588",
                Adresse = new Adresse { CodePostal = "63854", Ville = "Grenoble", Rue = "cours test", Numero = "58 bis" },
                LastGrade = new Grade { DateDerniereVisite = DateTime.Parse("01/01/20"), Note = 10, Commentaire = "nul" }
            });
            db.Restaurants.Add(new Restaurant() {
                Nom = "Test2",
                Email = "test@test.fr",
                Telephone = "25411225588",
                Adresse = new Adresse { CodePostal = "63854", Ville = "Grenoble", Rue = "cours test", Numero = "58 bis" },
                LastGrade = new Grade { DateDerniereVisite = DateTime.Parse("01/01/20"), Note = 10, Commentaire = "nul" }
            });
            db.SaveChanges();
            srv = new RestaurantsService(db);
        }


        [TestMethod()]
        public void CreateRestaurantTest() {
            try {
                var restaurant = new Restaurant() {
                    Nom = "Toscana",
                    Email = "test@test.fr",
                    Telephone = "123456789",
                    Adresse = new Adresse { CodePostal = "12345", Ville = "ville1", Rue = "cours long", Numero = "12" },
                    LastGrade = new Grade { DateDerniereVisite = DateTime.Parse("01/01/20"), Note = 10, Commentaire = "Top" }
                };
                int countBefore = srv.GetRestaurantsList().Count;
                srv.CreateRestaurant(restaurant);
                var listeAfter = srv.GetRestaurantsList();
                Assert.IsTrue(listeAfter.Count == countBefore + 1);
                var dernierResto = listeAfter.OrderByDescending(r => r.ID).First();
                Assert.IsTrue(dernierResto.Nom == "Toscana");
            } catch (Exception e) {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void UpdateRestaurantTest() {
            try {
                var restaurant = db.Restaurants.Find(1);
                restaurant.Details = "Test";
                srv.UpdateRestaurant(restaurant);
                Assert.IsTrue(srv.GetRestaurantById(1).Details == "Test");
            } catch (Exception e) {
                Assert.Fail();
            }
        }


        [TestMethod()]
        public void GetRestaurantsListTest() {
            try {
                var restaurants = srv.GetRestaurantsList();
                Assert.IsTrue(restaurants.Count > 0);
            } catch (Exception e) {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void GetRestaurantsListWithGradesTest() {
            try {
                var restaurants = srv.GetRestaurantsListWithGrades();
                Assert.IsTrue(restaurants.Count > 0);
                // Il peut y avoir des restaurants sans note, mais on suppose qu'au moins un restaurant a une note, pour pouvoir tester l'inclusion. C'est pour cela qu'on initialise avec un restaurant
                Assert.IsTrue(restaurants.Any(restaurants => restaurants.LastGrade != null));
            } catch (Exception e) {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void GetRestaurantsListWithRelationsTest() {
            try {
                var restaurants = srv.GetRestaurantsListWithRelations();
                Assert.IsTrue(restaurants.Count > 0);
                Assert.IsTrue(restaurants.Any(restaurants => restaurants.LastGrade != null));
                Assert.IsTrue(restaurants.All(restaurants => restaurants.Adresse != null));
            } catch (Exception e) {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void GetTopFiveWithGradesTest() {
            try {
                var restaurants = srv.GetTopFiveWithGrades();
                var nombre = restaurants.Count;
                Assert.IsTrue(nombre == db.Restaurants.Where(r => r.Adresse.Ville.ToLower() == "grenoble").Count() || nombre == 5);
            } catch (Exception e) {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void GetRestaurantByIdTest() {
            try {
                var restaurant = srv.GetRestaurantById(1);
                Assert.IsTrue(restaurant.ID == 1);
            } catch (Exception e) {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void GetRestaurantWithAdresseTest() {
            try {
                var restaurant = srv.GetRestaurantById(1);
                Assert.IsTrue(restaurant.ID == 1 && restaurant.Adresse != null);
            } catch (Exception e) {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void GetRestaurantWithRelationsTest() {
            try {
                var restaurant = srv.GetRestaurantById(1);
                Assert.IsTrue(restaurant.ID == 1 && restaurant.Adresse != null && restaurant.LastGrade != null);
            } catch (Exception e) {
                Assert.Fail();
            }
        }



        [TestMethod()]
        public void GradeRestaurantTest() {
            try {
                var note = new Grade { Note = 2, DateDerniereVisite = DateTime.Now, Commentaire = "très bien", RestaurantID = 1 };
                srv.GradeRestaurant(note);
                var restau = db.Restaurants.Include(r => r.LastGrade).FirstOrDefault(r => r.ID == 1);
                Assert.IsTrue(restau.LastGrade.Note == 2);
            } catch (Exception e) {
                Assert.Fail();
            }
        }



        [TestMethod()]
        public void DeleteRestaurantTest() {
            try {
                var countBefore = db.Restaurants.Count();
                srv.DeleteRestaurant(2);
                var countAfter = db.Restaurants.Count();
                Assert.IsTrue(countAfter == countBefore - 1);
            } catch (Exception e) {
                Assert.Fail();
            }
        }
    }
}
