using AutoMapper;
using BussinessLayer.DTOs.Beverages;
using DataLayer.Entities;

namespace BussinessLayer.AutoMapper.Beverages
{
    public class BeverageSizeMapper : Profile
    {
        public BeverageSizeMapper()
        {
            CreateMap<BeverageSize, BeverageSizeDTO>().ReverseMap();
        }
    }
}