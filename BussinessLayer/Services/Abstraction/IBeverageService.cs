using BussinessLayer.DTOs.Beverages;
using DataLayer.Entities;

namespace BussinessLayer.Services.Abstraction
{
    public interface IBeverageService
    {
        Task CreateAsync(CreateBeverageDTO createBeverageDTO);
        Task<IEnumerable<Beverage>> GetAllBeverages();
        Task UpdateBeverage(UpdateBeverageDTO updateBeverageDTO);
        Task<bool> DeleteBeverage(string id);
    }
}