using System.ComponentModel.DataAnnotations;
using System;

namespace TripMeOn.Models.PartnerProducts
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
