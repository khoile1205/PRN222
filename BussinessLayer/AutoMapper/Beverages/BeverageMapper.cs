using AutoMapper;
using BussinessLayer.DTOs.Beverages;
using DataLayer.Entities;

namespace BussinessLayer.AutoMapper.Beverages
{
    public class BeverageMapper : Profile
    {
        public BeverageMapper()
        {
            CreateMap<Beverage, CreateBeverageDTO>()
               .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Image))
               .ReverseMap()
               .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.ImageUrl));

            CreateMap<BeverageDetail, CreateBeverageDTO>().ReverseMap()
                .ForMember(dest => dest.BeverageId, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
        }
    }
}