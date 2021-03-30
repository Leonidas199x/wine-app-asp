using System.ComponentModel.DataAnnotations;

namespace wine_app.Models.Country
{
    public class EditableCountryViewModel
    {
        public int Id { get; set; }

        [Display(Name="Country"), Required]
        [ScaffoldColumn(true)]
        [StringLength(50, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
        public string Name { get; set; }

        public string Note { get; set; }
    }
}
