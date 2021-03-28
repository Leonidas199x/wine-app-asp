using AutoMapper;
using System.Collections.Generic;

namespace wine_app.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region country
            CreateMap<Domain.Country.Country, Models.Country.Country>();
            CreateMap<Domain.Country.Country, Models.Country.EditableCountryViewModel>().ReverseMap();
            #endregion
        }
    }
}
