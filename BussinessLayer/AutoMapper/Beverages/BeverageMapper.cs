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
            .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.BeverageDetails))
            .ReverseMap()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.ImageUrl));

            CreateMap<BeverageDetail, BeverageDetailDTO>().ReverseMap()
                .ForMember(dest => dest.BeverageId, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
        }
    }
}