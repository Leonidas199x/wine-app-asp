using System.ComponentModel.DataAnnotations;

namespace wine_app.Models.StopperType
{
    public class EditableStopperTypeViewModel
    {
        public int? Id { get; set; }

        [Display(Name = "Stopper Type"), Required]
        public string Name { get; set; }
    }
}
