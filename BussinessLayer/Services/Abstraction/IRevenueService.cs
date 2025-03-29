using BussinessLayer.DTOs.Revenues;
using Shared.Enums;

namespace BussinessLayer.Services.Abstraction
{
    public interface IRevenueService
    {
        Task<IEnumerable<RevenueDTO>> GetRevenues(RevenueRangeTypeEnum rangeType, DateTime? startTime, DateTime? endTime);
        (DateTime start, DateTime end) GetRevenueDateRange(RevenueRangeTypeEnum rangeType, DateTime? startTime, DateTime? endTime);
    }
}