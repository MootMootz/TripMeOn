using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
				new Destination { Id = 1, Country = "France", City = "Paris", Description = "4 days Tours", Region = "Ile De France" },
				new Destination { Id = 2, Country = "France", City = "Valensole", Description = "4 days Tours", Region = "South" },
				new Destination { Id = 3, Country = "UK", City = "London", Description = "4 daysTours", Region = "London" },				
				new Destination { Id = 4, Country = "Netherlands", City = "Amsterdam", Description = "3 days Tours", Region = "Western " },
				new Destination { Id = 5, Country = "Netherlands", City = "Giethoorn", Description = "3 days Tours", Region = "Overijssel" },
				new Destination { Id = 6, Country = "Turkey", City = "Istanbul", Description = "5 days Tours", Region = "north-western" },
				new Destination { Id = 7, Country = "Turkey", City = "Cappadocia", Description = "5 days Tours", Region = "Central Anatolia" },
				new Destination { Id = 8, Country = "Agentina", City = "Ushuaia", Description = "5 days Tours", Region = "Tierra del Fuego" },
				new Destination { Id = 9, Country = "Agentina", City = "city", Description = "5 days Tours", Region = "North" },
				new Destination { Id = 10, Country = "Belgium", City = "Ghent", Description = "5 days Tours", Region = "Flemish" },
				new Destination { Id = 11, Country = "Spain", City = "Bercelona", Description = "5 days Tours", Region = "Catalonia" },
				new Destination { Id = 12, Country = "Spain", City = "Ferrol ", Description = "5 days Tours", Region = "Galicia" },
				new Destination { Id = 13, Country = "Switzerland", City = "Zermatt", Description = "5 days Tours", Region = "Canton of Valais" },
				new Destination { Id = 14, Country = "Portugal", City = "Porto", Description = "5 days Tours", Region = "Porto" },
				new Destination { Id = 15, Country = "Greece", City = "Santorini", Description = "5 days Tours", Region = "Cyclades" },

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


			var tourPackages = new List<TourPackage>
			{
				new TourPackage { Id = 1, Name = "Bloomings in Paris", DestinationId = 1, ThemeId = 2, Price = 986 },
				new TourPackage { Id = 2, Name = "Confetti Field", DestinationId = 3, ThemeId = 2, Price = 1180 },

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
