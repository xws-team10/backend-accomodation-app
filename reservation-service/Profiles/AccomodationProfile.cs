using AutoMapper;
using reservation_service.DTO;
using reservation_service.Model;

namespace reservation_service.Profiles
{
    public class AccomodationProfile : Profile
    {
        public AccomodationProfile() {
            CreateMap<Reservation, AccomodationModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AccomodationId));
        }
        
    }
}
