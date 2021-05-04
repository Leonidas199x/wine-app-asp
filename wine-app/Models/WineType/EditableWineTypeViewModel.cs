using System.ComponentModel.DataAnnotations;

namespace wine_app.Models.WineType
{
    public class EditableWineTypeViewModel
    {
        public int? Id { get; set; }

        [Display(Name = "Wine Type"), Required]
        public string Name { get; set; }

        public string Note { get; set; }
    }
}
