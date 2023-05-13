using accomodation_service.Dtos;
using accomodation_service.Model;
using AutoMapper;

namespace accomodation_service.Profiles
{
    public class PlatformsProfile : Profile
    {
        public PlatformsProfile()
        {
            // Source -> Target
            CreateMap<Accomodation, AccomodationReadDto>();
            CreateMap<AccomodationReadDto, Accomodation>();
            CreateMap<AccomodationCreateDto, Accomodation>();
        }
    }
}