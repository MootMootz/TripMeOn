namespace TripMeOn.ViewModels
{
    public class ItemViewModel
    {
        public int? TourPackageId { get; set; }
        public string TourPackageName { get; set; }
        public double TourPackagePrice { get; set; }
        public int? AccomodationId { get; set; }
        public string AccomodationName { get; set; }
        public double AccomodationPrice { get; set; }
        public int? RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public double RestaurantPrice { get; set; }
        public int? TransportId { get; set; }
        public string TransportationType { get; set; }
        public double TransportationPrice { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
    }
}
