using System;

namespace wine_app.Domain.Grape
{
    public class Grape
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int GrapeColourId { get; set; }

        public string Note { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }
    }
}
