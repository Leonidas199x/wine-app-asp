using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace wine_app.Models.Region
{
    public class EditableRegionViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Region"), Required]
        public string Name { get; set; }

        public int CountryId { get; set; }

        [Display(Name = "Country")]
        public IEnumerable<SelectListItem> Countries { get; set; }

        [Display(Name = "Note")]
        public string Note { get; set; }

        [Display(Name = "Longitude")]
        public decimal Longitude { get; set; }

        [Display(Name = "Latitude")]
        public decimal Latitude { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }
    }
}
