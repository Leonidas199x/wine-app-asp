using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace wine_app.Models.Grape
{
    public class EditableGrapeViewModel
    {
        public int? Id { get; set; }

        [Display(Name = "Name"), Required]
        public string Name { get; set; }

        [Display(Name = "Grape Colour")]
        public int? GrapeColourId { get; set; }

        [Display(Name = "Grape Colour")]
        public IEnumerable<SelectListItem> GrapeColours { get; set; }

        [Display(Name = "Note")]
        public string Note { get; set; }
    }
}
