using System.ComponentModel.DataAnnotations;

namespace wine_app.Models.Grape
{
    public class EditableGrapeColourViewModel
    {
        public int? Id { get; set; }

        [Display(Name = "Colour"), Required]
        public string Colour { get; set; }
    }
}
