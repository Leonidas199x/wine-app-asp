using System.Collections.Generic;
using wine_app.Models.Country;
using System.Linq;

namespace wine_app.Mappers.Country
{
    public static class CountryMapper
    {

        public static CountriesViewModel Map(IEnumerable<Domain.Country.Country> value)
        {
            if(value == null)
            {
                return null;
            }

            return new CountriesViewModel
            {
                Countries = value.Select(x => Map(x)),
            };
        }

        public static Models.Country.Country Map(Domain.Country.Country value)
        {
            if(value == null)
            {
                return null;
            }

            return new Models.Country.Country
            {
                Id = value.Id,
                Name = value.Name,
            };
        }
    }
}
