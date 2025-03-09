using AutoMapper;
using BussinessLayer.DTOs.Beverages;
using DataLayer.Entities;

namespace BussinessLayer.AutoMapper.Beverages
{
    public class BeverageMapper : Profile
    {
        public BeverageMapper()
        {
            CreateMap<Beverage, CreateBeverageDTO>().ReverseMap();

            CreateMap<BeverageDetail, CreateBeverageDTO>().ReverseMap()
                .ForMember(dest => dest.BeverageId, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
        }
    }
}