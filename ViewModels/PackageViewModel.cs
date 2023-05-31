using Microsoft.AspNetCore.Mvc.Rendering;

namespace TripMeOn.ViewModels
{
    public class PackageViewModel
    {
        public string Name { get; set; }
        public int DestinationId { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public int ThemeId { get; set; }
        public string Theme { get; set; }
        public double Price { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string PhotoUrl { get; set; }
        public SelectList Destinations { get; set; }
        public SelectList Themes { get; set; }
    }
}
