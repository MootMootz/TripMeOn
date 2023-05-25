using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TripMeOn.Models.PartnerProducts;
using TripMeOn.Models.Products;
using TripMeOn.Models.Users;

namespace TripMeOn.Models
{
    public class BddContext: DbContext
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user id=root;password=twitwiboo;database=TripMeOn");
        }
        public void InitializeDb()
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
            
            var clients = new List<Client>
            {
                new Client { Id = 1, Name = "Phone Mo", FirstName = "Nway Nway", Email = "nnpm.gmail.com", Password = "123456", Address= "21 bis Rue des Rossays,91600, France",PhoneNumber="0778146263",ClientType="Instagrammer"},
                new Client { Id = 2, Name = "Kanobi", FirstName = "Obiwan", Email = "oldman.gmail.com", Password = "3546246", Address= "66 Clover Road, 43k67H,UK ",PhoneNumber="057789021",ClientType="TikToker"},

            };

            var employees = new List<Employee>
            {
                 new Employee { Id = 1, Name = "Lieby", FirstName = "Karen", Email = "mightK.gmail.com", Password = "4677646", Address= "3 Rue de Victor Hugo, 21670, France",PhoneNumber="0676455781",JobTitle="Admin"},
            };

            var partners = new List<Partner>
            {
                 new Partner { Id = 1, Name = "BeauGoud", FirstName = "Sedar", Email = "bogosse.gmail.com", Password = "0597970983", Address= "123 Avenue Fromenteau,45091, France",PhoneNumber="076543211",CompanyName="EasyGo"},
            };

            var destinations = new List<Destination>
            {
                new Destination { Id = 1, Country = "France", City = "Paris", Description = "3 days", Region = "IDF" },
                new Destination { Id = 2, Country = "UK", City = "London", Description = "3 days", Region = "London" },

            };

           
            var themes = new List<Theme>
            {
                new Theme { Id = 1, Name = "Patrimonial" },
                new Theme { Id = 2, Name = "Adventure" },
            };
           

            var tourPackages = new List<TourPackage>
            {
                new TourPackage { Id = 1, Name = "blabla", DestinationId = 1, ThemeId = 1, Price = 599 },
                new TourPackage { Id = 2, Name = "blabla", DestinationId = 1, ThemeId = 1, Price = 599 },

            };
            this.Clients.AddRange(clients);
            this.Employees.AddRange(employees);
            this.Partners.AddRange(partners);
            this.Destinations.AddRange(destinations);
            this.Themes.AddRange(themes);
            this.TourPackages.AddRange(tourPackages);
            this.SaveChanges();
        }

        
    }
}
