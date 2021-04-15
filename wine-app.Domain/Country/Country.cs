using System;

namespace wine_app.Domain.Country
{
    public class Country
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string IsoCode { get; set; }

        public string Note { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }
    }
}
