using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace wine_app.Models.Region
{
    public class RegionViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Region")]
        public string Name { get; set; }

        public string IsoCode { get; set; }

        public int CountryId { get; set; }

        public string Country { get; set; }

        public string Note { get; set; }

        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public IEnumerable<string> Coordinates { get; set; }
    }
}
