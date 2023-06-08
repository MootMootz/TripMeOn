using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using TripMeOn.BL;
using TripMeOn.Helper;
using TripMeOn.Models.Order;
using TripMeOn.Models.PartnerProducts;
using TripMeOn.Models.Products;
using TripMeOn.Models.Users;

namespace TripMeOn.Models
{
    /// <summary>
    /// CLass pour instancier les services, utilisateurs, et tous les atributs des objets créés, dans la bdd
    /// </summary>
    public class BddContext : DbContext
    {
        public DbSet<TourPackage> TourPackages { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Accomodation> Accomodations { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Transportation> Transportations { get; set; }
        public DbSet<TimePeriod> TimePeriods { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseMySql("server=localhost;user id=root;password=1530;database=TripMeOn");

        }

        public void InitializeDb()
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();

            var clients = new List<Client>
            {
                new Client { Id = 1, LastName = "Phone Mo", FirstName = "Nway Nway", Nickname = "NN", Email = "nnpm.gmail.com", Password = UserService.EncodeMD5("1111"), Address= "21 bis Rue des Rossays, 91600, France",PhoneNumber="0778146263",ClientType="Instagrammer"},
                new Client { Id = 2, LastName = "Dupin", FirstName = "Frédéric", Nickname = "FD", Email = "frederic.dupin.pro@gmail.com", Password = UserService.EncodeMD5("3333"), Address= "8 Rue Saint-Michel 93170, France",PhoneNumber="0677887855",ClientType="Blogger"},
                new Client { Id = 3, LastName = "Dumas", FirstName = "Anthony", Nickname = "AD", Email = "anthony.dumas30@gmail.com", Password = UserService.EncodeMD5("5555"), Address = "10 Rue de label kebab, 75002, France", PhoneNumber ="0611843877", ClientType = "Tiktoker" },
                new Client { Id = 4, LastName = "Aitcheou", FirstName = "David", Nickname = "DA", Email = "gilchristaitcheou@gmail.com", Password = UserService.EncodeMD5("7777"), Address = "456 Elm Avenue, Anycity, USA", PhoneNumber = "0650556784", ClientType = "Influencer" },
            };

            var employees = new List<Employee>
            {
                new Employee { Id = 1, LastName = "Lieby", FirstName = "Karen", Nickname = "QQs", Email = "karen.lieby@outlook.com", Password = UserService.EncodeMD5("2222"), Address= "192 ancienne route de Quissac, 30250, France",PhoneNumber="0676455781",Role="Admin"},
                new Employee { Id = 2, LastName = "Lestieux", FirstName = "Florian", Nickname = "FF", Email = "chickenmaster.gmail.com", Password = UserService.EncodeMD5("3333"), Address= "66 avenue des champs bleus, 44000, France ",PhoneNumber="0621685505", Role="Manager"},
                new Employee { Id = 4, LastName = "Phone Mo", FirstName = "Nway Nway", Nickname = "PMN", Email = "nnpm.hotmail.com", Password = UserService.EncodeMD5("8888"), Address = "12 rue anthony dumas 91600, France", PhoneNumber = "0778146263", Role = "Staff" },
            };

            var partners = new List<Partner>
            {
                 new Partner { Id = 1, LastName = "Korkmaz", FirstName = "Timur", Nickname = "TIM", Email = "easygo@gmail.com", Password = UserService.EncodeMD5("0000"), Address= "123 Avenue Fromenteau,45091, France",PhoneNumber="0765432110",CompanyName="EasyGo"},
                 new Partner { Id = 2, LastName = "El Aissaoui", FirstName = "Raounak", Nickname = "REA", Email = "tazavoyage@gmail.com", Password = UserService.EncodeMD5("0000"), Address= "10 av Salta, Salta, Argentine",PhoneNumber="0665609582",CompanyName="TazaVoyage"},
            };

            var destinations = new List<Destination>
            {
                new Destination { Id = 1, Country = "France", City = "Paris", Region = "Ile De France" },
                new Destination { Id = 2, Country = "France", City = "Valensole", Region = "South" },
                new Destination { Id = 3, Country = "UK", City = "London",Region = "London"},
                new Destination { Id = 4, Country = "Netherlands", City = "Amsterdam", Region = "Western " },
                new Destination { Id = 5, Country = "Netherlands", City = "Giethoorn", Region = "Overijssel"},
                new Destination { Id = 6, Country = "Turkey", City = "Istanbul", Region = "north-western" },
                new Destination { Id = 7, Country = "Turkey", City = "Cappadocia", Region = "Central Anatolia" },
                new Destination { Id = 8, Country = "Argentina", City = "Ushuaia", Region = "Tierra del Fuego"  },
                new Destination { Id = 9, Country = "Argentina", City = "Jujuy", Region = "North" },
                new Destination { Id = 10, Country = "Belgium", City = "Ghent",Region = "Flemish" },
                new Destination { Id = 11, Country = "Spain", City = "Bercelona", Region = "Catalonia" },
                new Destination { Id = 12, Country = "Spain", City = "Ferrol ", Region = "Galicia" },
                new Destination { Id = 13, Country = "Switzerland", City = "Zermatt",  Region = "Canton of Valais" },
                new Destination { Id = 14, Country = "Portugal", City = "Porto",Region = "Porto"},
                new Destination { Id = 15, Country = "Greece", City = "Santorini", Region = "Cyclades" },

            };

            var timePeriods = new List<TimePeriod>
            {
                new TimePeriod { Id = 1,StartMonth = 1, EndMonth = 2 },
                new TimePeriod { Id = 2,StartMonth = 2, EndMonth = 3 },
                new TimePeriod { Id = 3,StartMonth = 3, EndMonth = 4 },
                new TimePeriod { Id = 4,StartMonth = 4, EndMonth = 5 },
                new TimePeriod { Id = 5,StartMonth = 5, EndMonth = 6 },
                new TimePeriod { Id = 6,StartMonth = 6, EndMonth = 7 },
                new TimePeriod { Id = 7,StartMonth  =7, EndMonth =8 },
                new TimePeriod { Id = 8,StartMonth =8, EndMonth =9 },
                new TimePeriod { Id = 9,StartMonth =9, EndMonth =10 },
                new TimePeriod { Id = 10,StartMonth =10, EndMonth =11 },
                new TimePeriod { Id = 11,StartMonth =11, EndMonth =12 },
                new TimePeriod { Id = 12,StartMonth =12, EndMonth =1 },
            };

            var themes = new List<Theme>
            {
                new Theme { Id = 1, Name = "Trekking" },
                new Theme { Id = 2, Name = "Floral" },
                new Theme { Id = 3, Name = "Cultural" },
                new Theme { Id = 4, Name = "Heritage" },
                new Theme { Id = 5, Name = "Snap and relax" },
                new Theme { Id = 6, Name = "InstaSpots" },
                new Theme { Id = 7, Name = "Xtreme Adventure" },
                new Theme { Id = 8, Name = "Beach" },
                new Theme { Id = 9, Name = "Culinary" },
                new Theme { Id = 10, Name = "Colors" },
            };
            var images = new List<Image>
            {
                new Image { Id = 1,Url="/images/cover/pk1.jpg"},
                new Image { Id = 2,Url="/images/cover/pk2.jpg"},
                new Image { Id = 3,Url="/images/cover/pk3.jpg"},
                new Image { Id = 4,Url="/images/cover/pk4-fall.jpg"},
                new Image { Id = 5,Url="/images/cover/pk5.jpg"},
                new Image { Id = 6,Url="/images/cover/pk6.jpg"},
                new Image { Id = 7,Url="/images/cover/pk7.jpg"},
                new Image { Id = 8,Url="/images/cover/pk8.jpg"},
                new Image { Id = 9,Url="/images/cover/pk9.jpg"},
                new Image { Id = 10,Url="/images/cover/pk10.jpg"},
                new Image { Id = 11, Url="/images/cover/p11.jpg"},
                new Image { Id = 12, Url="/images/cover/pk12.jpg"},
                new Image { Id = 13, Url="/images/accomodation/htlSalta.jpg"},
                new Image { Id = 14, Url="/images/accomodation/htlJujuy.jpg"},
                new Image { Id = 15, Url="/images/accomodation/htlUshuaia.jpg"},
                new Image { Id = 16, Url="/images/accomodation/htlUshuaia2.jpg"},
                new Image { Id = 17, Url="/images/accomodation/htlCalafate.jpg"},
                new Image { Id = 18, Url="/images/accomodation/htlParis1.jpg"},
                new Image { Id = 19, Url="/images/accomodation/htlParis2.jpg"},
                new Image { Id = 20, Url="/images/accomodation/htlParis3.jpg"},
                new Image { Id = 21, Url="/images/restaurant/restoJujuy.jpg"},
                new Image { Id = 22, Url="/images/restaurant/restoSalta.jpg"},
                new Image { Id = 23, Url="/images/restaurant/restoUshuaia.jpg"},
                new Image { Id = 24, Url="/images/restaurant/restoUshuaia2.jpg"},
                new Image { Id = 25, Url= "/images/restaurant/restoParis.jpg"},
                new Image { Id = 26, Url= "/images/restaurant/restoParis2.jpg"},
                new Image { Id = 27, Url= "/images/restaurant/restoParis3.jpg"},
                new Image { Id = 28, Url= "/images/transports/pickUp.jpg"},
                new Image { Id = 29, Url= "/images/transports/pickUp2.jpg"},
                new Image { Id = 30, Url= "/images/transports/bike.jpg"},
                new Image { Id = 31, Url= "/images/transports/bike2.jpg"},
                new Image { Id = 32, Url= "/images/transports/jeep.jpg"},
                new Image { Id = 33, Url= "/images/cover/pk13.jpg"},
                new Image { Id = 34, Url= "/images/p13/turkey1.jpg"},
                new Image { Id = 35, Url= "/images/p13/turkey2.jpg"},
                new Image { Id = 36, Url= "/images/p13/turkey3.jpg"}
            };

            var tourPackages = new List<TourPackage>            {

                new TourPackage { Id = 1, Name = "Magnolia and early Cherry in Paris", DestinationId = 1, ThemeId = 2,Description="4 days tour",TimePeriodId=3, Price = 986,ImageId=1 },
                new TourPackage { Id = 2, Name = "Lavender Fields of Provence", DestinationId = 2, ThemeId = 2,Description="4 days tour",TimePeriodId=7, Price = 1180,ImageId=2 },
                new TourPackage { Id = 3, Name = "Confetti Field of Pershore ", DestinationId = 3, ThemeId = 2,Description="4 days tour",TimePeriodId=6, Price = 1180,ImageId=3 },
                new TourPackage { Id = 4, Name = "Colmer in Autumn", DestinationId = 1, ThemeId = 10,Description="4 days tour",TimePeriodId=11,Price= 776,ImageId = 4 },
                new TourPackage { Id = 5, Name = "Wisteria and Sakura in Paris", DestinationId = 1, ThemeId = 2,Description="4 days tour",TimePeriodId=4,Price= 776,ImageId = 5},
                new TourPackage { Id = 6, Name = "May Bloomings in Paris", DestinationId = 1, ThemeId = 2,Description="4 days tour",TimePeriodId=5,Price= 776,ImageId = 6 },
                new TourPackage { Id = 7, Name = "Medieval Splendors in Belgium", DestinationId = 10, ThemeId = 3,Description="4 days tour",TimePeriodId=1,Price= 776,ImageId = 7 },
                new TourPackage { Id = 8, Name = "Floating village in tulip season", DestinationId = 5, ThemeId = 2,Description="4 days tour",TimePeriodId=4,Price= 776,ImageId = 8 },
                new TourPackage { Id = 10, Name = "Sweet December Colmer", DestinationId = 1, ThemeId = 4,Description="4 days tour",TimePeriodId=12,Price= 776,ImageId = 10 },
                new TourPackage { Id = 11, Name = "Colored mountains in Argentina", DestinationId = 8, ThemeId = 10, Description="5 days tour", TimePeriodId=5, Price= 650, ImageId= 11 },
                new TourPackage { Id = 12, Name = "Wildlife at the End of the World", DestinationId = 9, ThemeId = 7, Description= "5 days tour", TimePeriodId=2, Price=880, ImageId= 12},
                new TourPackage { Id = 13, Name = "Flying away in Turkey", DestinationId = 7, ThemeId = 7, Description= "4 days tour", TimePeriodId=10, Price=760, ImageId= 33}
            };


            var accomodation = new List<Accomodation> {

                new Accomodation { Id =  1, Capacity = 2, Name = "Chez Exequiel", Type= "Appartment", Price= 16, StartDate = new DateTime(2023, 04, 01), EndDate = new DateTime(2023, 11, 30), Description= "Beautiful appartment in the city center of Salta", PartnerId = 2, DestinationId = 8, ImageId = 13},
                new Accomodation { Id =  2, Capacity = 30, Name = "Colores de Purmamarca", Type= "Hotel", Price= 20, StartDate = new DateTime(2023, 04, 01), EndDate = new DateTime(2023, 11, 30), Description= "Beautiful hotel in the city center of Jujuy", PartnerId = 2, DestinationId = 8, ImageId = 14},
                new Accomodation { Id =  3, Capacity = 4, Name = "Mountain paradise", Type= "Maison", Price= 60, StartDate = new DateTime(2023, 10, 01), EndDate = new DateTime(2024,04,01), Description= "Cabane in the middle of the mountains", PartnerId = 2, DestinationId = 8, ImageId = 15},
                new Accomodation { Id =  4, Capacity = 35, Name = "Trip me Here", Type= "Hotel", Price= 50, StartDate = new DateTime(2023, 10, 01), EndDate = new DateTime(2024, 04, 30), Description= "Amazing hotel in Ushuaia", PartnerId = 2, DestinationId = 8, ImageId = 16},
                new Accomodation { Id =  5, Capacity = 5, Name = "Chez Posada", Type= "Hotel", Price= 70, StartDate = new DateTime(2023, 09, 01), EndDate = new DateTime(2024, 05, 01), Description= "Best view of the Lake Argentino from the dinning room", PartnerId = 2, DestinationId = 8, ImageId = 17},
                new Accomodation { Id =  6, Capacity = 6, Name = "Esplendor", Type= "Appartment", Price= 120, StartDate = new DateTime(2023, 01, 01), EndDate = new DateTime(2023, 12, 31), Description= "Comfortable appartement in the center of Paris", PartnerId = 1, DestinationId = 1, ImageId = 18},
                new Accomodation { Id =  7, Capacity = 20, Name = "The View", Type= "Hotel", Price= 90, StartDate = new DateTime(2023, 01, 01), EndDate = new DateTime(2023, 12, 31), Description= "Hotel in the city center with an excellent view", PartnerId = 1, DestinationId = 1, ImageId = 19},
                new Accomodation { Id =  8, Capacity = 2, Name = "WaterTrip", Type= "Boat", Price= 60, StartDate = new DateTime(2023, 01, 01), EndDate = new DateTime(2023, 12, 31), Description= "Relaxing experience of sleeping in a boat at the Seine", PartnerId = 1, DestinationId = 1, ImageId = 20},
            };

            var restaurant = new List<Restaurant> {
                new Restaurant { Id = 1, Type = "Restaurant", Name = "Cozy Place", StartDate = new DateTime(2023, 01, 01), EndDate = new DateTime(2023, 12, 31), Price= 40, PartnerId= 1, DestinationId=1, ImageId= 26, Description= "Three course meal with 1 drinks" },
                new Restaurant { Id = 2, Type = "Restaurant", Name = "The Terrasse", StartDate = new DateTime(2023, 01, 01), EndDate = new DateTime(2023, 12, 31), Price= 35, PartnerId= 1, DestinationId=1, ImageId= 25, Description= "Three course meal with 1 drinks" },
                new Restaurant { Id = 3, Type = "Restaurant", Name= "Tree", StartDate = new DateTime(2023, 01, 01), EndDate = new DateTime(2023, 12, 31), Price= 35, PartnerId= 1, DestinationId=1, ImageId= 27, Description= "Three course meal with 1 drinks" },
                new Restaurant { Id = 4, Type = "Coffee House", Name = "El Club", StartDate = new DateTime(2023, 09, 01), EndDate = new DateTime(2024, 05, 31), Price= 10, PartnerId= 2, DestinationId=8, ImageId= 23, Description= "American breakfast" },
                new Restaurant { Id = 5, Type = "Cofee House", Name = "Mi Casa es tu Casa", StartDate = new DateTime(2023, 09, 01), EndDate = new DateTime(2024, 05, 31), Price= 15, PartnerId= 2, DestinationId=8, ImageId= 24, Description = "Continental breakfast" },
                new Restaurant { Id = 6, Type = "Restaurant", Name = "Bambolero", StartDate = new DateTime(2023, 03, 01), EndDate = new DateTime(2023, 10, 31), Price= 12, PartnerId= 2, DestinationId=8, ImageId= 21, Description = "Two course meal with 2 drinks" },
                new Restaurant { Id = 7, Type = "Restaurant", Name = "El Patio", StartDate = new DateTime(2023, 03, 01), EndDate = new DateTime(2023, 10, 31), Price= 20, PartnerId= 2, DestinationId=8, ImageId= 22, Description= "Three course meal with 1 drink"}
            };

            var transport = new List<Transportation> {
                new Transportation {  Id = 1, Type = "car", Price = 40, StartDate = new DateTime(2023, 03, 01), EndDate = new DateTime(2023, 10, 31), PartnerId = 2, DestinationId = 8, ImageId= 28, Description = "Pick up and drop off at the airport of Salta, north of Argentina" },
                new Transportation {  Id = 2, Type = "car", Price = 50, StartDate = new DateTime(2023, 09, 01), EndDate = new DateTime(2024, 05, 31), PartnerId = 2, DestinationId = 8, ImageId= 29, Description = "Pick up and drop off at the airport of Ushuaia, south of Argentina" },
                new Transportation {  Id = 5, Type = "car", Price = 30, StartDate = new DateTime(2023, 09, 01), EndDate = new DateTime(2024, 05, 31), PartnerId = 2, DestinationId = 8, ImageId= 32, Description = "Pick up and drop off at the airport of Ushuaia, south of Argentina" },
                new Transportation {  Id = 3, Type = "bike", Price = 20, StartDate = new DateTime(2023, 01, 01), EndDate = new DateTime(2023, 12, 31), PartnerId = 1, DestinationId = 1, ImageId= 31, Description = "Rent a bike for as many days as you need to in Paris" },
                new Transportation {  Id = 4, Type = "bike", Price = 30, StartDate = new DateTime(2023, 01, 01), EndDate = new DateTime(2023, 12, 31), PartnerId = 1, DestinationId = 1, ImageId= 30, Description = "Rent an electric bike in Paris" }

            };

            this.Clients.AddRange(clients);
            this.Employees.AddRange(employees);
            this.Partners.AddRange(partners);
            this.Destinations.AddRange(destinations);
            this.Themes.AddRange(themes);
            this.TourPackages.AddRange(tourPackages);
            this.TimePeriods.AddRange(timePeriods);
            this.Images.AddRange(images);
            this.Accomodations.AddRange(accomodation);
            this.Restaurants.AddRange(restaurant);
            this.Transportations.AddRange(transport);
            this.SaveChanges();
        }

    }
}


