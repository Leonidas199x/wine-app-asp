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

        [Display(Name = "Country"), Required]
        public int CountryId { get; set; }

        [Display(Name = "Country")]
        public IEnumerable<SelectListItem> Countries { get; set; }

        [Display(Name = "Note")]
        public string Note { get; set; }

        [Display(Name = "Longitude")]
        [DisplayFormat(DataFormatString = "{0:F9}", ApplyFormatInEditMode = true)]
        public decimal Longitude { get; set; }

        [Display(Name = "Latitude")]
        [DisplayFormat(DataFormatString = "{0:F9}", ApplyFormatInEditMode = true)]
        public decimal Latitude { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }
    }
}
