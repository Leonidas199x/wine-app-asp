using AutoMapper;

namespace wine_app.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Domain
            CreateMap(typeof(Models.PagedList<>), typeof(Domain.PagedList<>)).ReverseMap();
            #endregion

            #region country
            CreateMap<Domain.Country.Country, Models.Country.Country>().ReverseMap();
            CreateMap<Domain.Country.Country, Models.Country.EditableCountryViewModel>().ReverseMap();
            CreateMap<Models.Country.CountryViewModel, Domain.Country.Country>().ReverseMap();
            #endregion

            #region grape
            CreateMap<Domain.Grape.Grape, Models.Grape.Grape>().ReverseMap();
            CreateMap<Domain.Grape.Grape, Models.Grape.GrapeViewModel>().ReverseMap();
            CreateMap<Models.Grape.EditableGrapeViewModel, Domain.Grape.Grape>().ReverseMap();
            CreateMap<Domain.Grape.GrapeColour, Models.Grape.GrapeColour>().ReverseMap();
            CreateMap<Domain.Grape.GrapeColour, Models.Grape.EditableGrapeColourViewModel>().ReverseMap();
            #endregion

            #region Region
            CreateMap<Domain.Region.Region, Models.Region.RegionViewModel>().ReverseMap();
            CreateMap<Domain.Region.Region, Models.Region.EditableRegionViewModel>().ReverseMap();
            #endregion
        }
    }
}
