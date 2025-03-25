using AutoMapper;
using BussinessLayer.DTOs.ShiftStaff;
using DataLayer.Entities;
using DataLayer.Enums;

namespace BussinessLayer.AutoMapper.Salary
{
    public class SalaryProfile : Profile
    {
        public SalaryProfile()
        {
            CreateMap<ShiftStaff, ShiftStaffDTO>()
                .ForMember(dest => dest.ShiftCode,
                    opt => opt.MapFrom(src => src.Shift != null ? src.Shift.ShiftCode : default))
                .ForMember(dest => dest.ShiftDescription,
                    opt => opt.MapFrom(src => src.Shift != null ? src.Shift.Description : null))
                .ForMember(dest => dest.StartTime,
                    opt => opt.MapFrom(src => src.Shift != null ? src.Shift.StartTime : TimeSpan.Zero))
                .ForMember(dest => dest.EndTime,
                    opt => opt.MapFrom(src => src.Shift != null ? src.Shift.EndTime : TimeSpan.Zero))
                .ForMember(dest => dest.ShiftType,
                    opt => opt.MapFrom(src => src.Shift != null ? src.Shift.ShiftType : default))
                .ForMember(dest => dest.ShiftDate,
                    opt => opt.MapFrom(src => src.ShiftDate));

        }
    }
}