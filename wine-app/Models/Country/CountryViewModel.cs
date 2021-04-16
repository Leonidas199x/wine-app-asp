using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace wine_app.Models.Country
{
    public class CountryViewModel
    {
        public int? Id { get; set; }

        [Display(Name = "Country"), Required]
        public string Name { get; set; }

        [Display(Name = "ISO Code"), Required]
        public string IsoCode { get; set; }

        public string Note { get; set; }

        public IEnumerable<string> Coordinates { get; set; }
    }
}
