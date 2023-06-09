﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using TripMeOn.Helper;

namespace TripMeOn.Models.Products
{
    public class TourPackage
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }             
        public int DestinationId { get; set; }
        public virtual Destination Destination { get; set; }
        public int ThemeId { get; set; }
        public virtual Theme Theme { get; set; }
        public string Description { get; set; }
        public int TimePeriodId { get; set; }
        public virtual TimePeriod TimePeriod { get; set; }
        public int? ImageId { get; set; }
        public virtual Image Image { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public double Price { get; set; }

        public override string ToString()
        {
            if (Price % 1 == 0)
            {
                return Price.ToString("0") + " €";
            }
            else
            {
                return Price.ToString("0.00") + " €";
            }
        }
    }
}
