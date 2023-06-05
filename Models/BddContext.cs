using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TripMeOn.BL;
using TripMeOn.Helper;
using TripMeOn.Models.Order;
using TripMeOn.Models.PartnerProducts;
using TripMeOn.Models.Products;
using TripMeOn.Models.Users;

namespace TripMeOn.Models
{
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
                new Client { Id = 1, LastName = "Phone Mo", FirstName = "Nway Nway", Nickname = "NN", Email = "nnpm.gmail.com", Password = UserService.EncodeMD5("1111"), Address= "21 bis Rue des Rossays,91600, France",PhoneNumber="0778146263",ClientType="Instagrammer"},
                new Client { Id = 2, LastName = "Kanobi", FirstName = "Obiwan", Email = "oldman.gmail.com", Password = "3546246", Address= "66 Clover Road, 43k67H,UK ",PhoneNumber="057789021",ClientType="TikToker"},

            };

            var employees = new List<Employee>
            {
                 new Employee { Id = 1, LastName = "Lieby", FirstName = "Karen", Nickname = "QQs", Email = "karen.lieby@outlook.com", Password = UserService.EncodeMD5("2222"), Address= "192 ancienne route de Quissac, 30250, France",PhoneNumber="0676455781",Role="Admin"},
            };

            var partners = new List<Partner>
            {
                 new Partner { Id = 1, LastName = "BeauGoud", FirstName = "Sedar", Nickname = "BG", Email = "bogosse.gmail.com", Password = UserService.EncodeMD5("0000"), Address= "123 Avenue Fromenteau,45091, France",PhoneNumber="076543211",CompanyName="EasyGo"},
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
                new Destination { Id = 8, Country = "Agentina", City = "Ushuaia", Region = "Tierra del Fuego"  },
                new Destination { Id = 9, Country = "Agentina", City = "city", Region = "North" },
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
                new Theme { Id = 7, Name = "Nature" },
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
                new Image { Id = 10,Url="/images/cover/pk10.jpg"}
            };

            var tourPackages = new List<TourPackage>
            {

                new TourPackage { Id = 1, Name = "Magnolia and early Cherry in Paris", DestinationId = 1, ThemeId = 2,Description="4 days tour",TimePeriodId=3, Price = 986,ImageId=1 },
                new TourPackage { Id = 2, Name = "Lavender Fields of Provence", DestinationId = 1, ThemeId = 2,Description="4 days tour",TimePeriodId=7, Price = 1180,ImageId=2 },
                new TourPackage { Id = 3, Name = "Confetti Field of Pershore ", DestinationId = 3, ThemeId = 2,Description="4 days tour",TimePeriodId=6, Price = 1180,ImageId=3 },
                new TourPackage { Id = 4, Name = "Colmer in Autumn", DestinationId = 1, ThemeId = 10,Description="4 days tour",TimePeriodId=11,Price= 776,ImageId = 4 },
                new TourPackage { Id = 5, Name = "Wisteria and Sakura in Paris", DestinationId = 1, ThemeId = 2,Description="4 days tour",TimePeriodId=4,Price= 776,ImageId = 5},
                new TourPackage { Id = 6, Name = "May Bloomings in Paris", DestinationId = 1, ThemeId = 2,Description="4 days tour",TimePeriodId=5,Price= 776,ImageId = 6 },
                new TourPackage { Id = 7, Name = "Medieval Splendors in Belgium", DestinationId = 10, ThemeId = 3,Description="4 days tour",TimePeriodId=12,Price= 776,ImageId = 7 },
                new TourPackage { Id = 8, Name = "Floating village in tulip season", DestinationId = 5, ThemeId = 2,Description="4 days tour",TimePeriodId=4,Price= 776,ImageId = 8 },
                new TourPackage { Id = 9, Name = "Springtime Serenade in Colmer", DestinationId = 1, ThemeId = 10,Description="4 days tour",TimePeriodId=4,Price= 776,ImageId = 9 },
                new TourPackage { Id = 10, Name = "Sweet December Colmer", DestinationId = 1, ThemeId = 4,Description="4 days tour",TimePeriodId=11,Price= 776,ImageId = 10 }
            };



            this.Clients.AddRange(clients);
            this.Employees.AddRange(employees);
            this.Partners.AddRange(partners);
            this.Destinations.AddRange(destinations);
            this.Themes.AddRange(themes);
            this.TourPackages.AddRange(tourPackages);
            this.TimePeriods.AddRange(timePeriods);
            this.Images.AddRange(images);
            this.SaveChanges();
        }

    }
}


