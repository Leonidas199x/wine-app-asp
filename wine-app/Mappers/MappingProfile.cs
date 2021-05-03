using AutoMapper;

namespace wine_app.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap(typeof(Models.PagedList<>), typeof(DataContract.PagedList<>)).ReverseMap();

            #region country
            CreateMap<DataContract.Country, Models.Country.Country>().ReverseMap();
            CreateMap<DataContract.Country, Models.Country.EditableCountryViewModel>().ReverseMap();
            CreateMap<Models.Country.CountryViewModel, DataContract.Country>().ReverseMap();
            #endregion

            #region grape
            CreateMap<DataContract.Grape, Models.Grape.Grape>().ReverseMap();
            CreateMap<DataContract.Grape, Models.Grape.GrapeViewModel>().ReverseMap();
            CreateMap<Models.Grape.EditableGrapeViewModel, DataContract.Grape>().ReverseMap();
            CreateMap<DataContract.GrapeColour, Models.Grape.GrapeColour>().ReverseMap();
            CreateMap<DataContract.GrapeColour, Models.Grape.EditableGrapeColourViewModel>()
                .ReverseMap();
            #endregion

            #region Region
            CreateMap<DataContract.Region, Models.Region.RegionViewModel>().ReverseMap();
            CreateMap<DataContract.Region, Models.Region.EditableRegionViewModel>().ReverseMap();
            #endregion

            #region Drinker
            CreateMap<DataContract.Drinker, Models.Drinker.Drinker>().ReverseMap();
            CreateMap<DataContract.Drinker, Models.Drinker.EditableDrinkerViewModel>().ReverseMap();
            #endregion
        }
    }
}
