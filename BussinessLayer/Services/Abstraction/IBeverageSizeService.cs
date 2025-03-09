using BussinessLayer.DTOs.Beverages;

namespace BussinessLayer.Services.Abstraction
{
    public interface IBeverageSizeService
    {
        Task<IEnumerable<BeverageSizeDTO>> GetAllBeverageSize();
    }
}