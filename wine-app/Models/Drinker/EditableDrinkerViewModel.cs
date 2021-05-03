using System.ComponentModel.DataAnnotations;

namespace wine_app.Models.Drinker
{
    public class EditableDrinkerViewModel
    {
        public int? Id { get; set; }

        [Display(Name = "Drinker"), Required]
        public string Name { get; set; }
    }
}
