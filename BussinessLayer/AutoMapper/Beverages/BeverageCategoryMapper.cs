using AutoMapper;
using BussinessLayer.DTOs.Beverages;
using DataLayer.Entities;

namespace BussinessLayer.AutoMapper.Beverages
{
    public class BeverageCategoryMapper : Profile
    {
        public BeverageCategoryMapper()
        {
            CreateMap<BeverageCategory, BeverageCategoryDTO>().ReverseMap();
        }
    }
}