using AutoMapper;

namespace wine_app.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region country
            CreateMap<Domain.Country.Country, Models.Country.Country>().ReverseMap();
            CreateMap<Domain.Country.Country, Models.Country.EditableCountryViewModel>().ReverseMap();
            #endregion

            #region grape
            CreateMap<Domain.Grape.GrapeColour, Models.Grape.GrapeColour>().ReverseMap();
            #endregion
        }
    }
}
