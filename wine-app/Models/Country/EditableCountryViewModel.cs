using System.ComponentModel.DataAnnotations;

namespace wine_app.Models.Country
{
    public class EditableCountryViewModel
    {
        public int Id { get; set; }

        [Display(Name="Country"), Required]
        public string Name { get; set; }

        public string Note { get; set; }
    }
}
