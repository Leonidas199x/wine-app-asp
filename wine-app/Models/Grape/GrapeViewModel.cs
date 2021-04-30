using System;

namespace wine_app.Models.Grape
{
    public class GrapeViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? GrapeColourId { get; set; }

        public string GrapeColourString { get; set; }

        public string Note { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }
    }
}
