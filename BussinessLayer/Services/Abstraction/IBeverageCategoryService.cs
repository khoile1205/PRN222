using BussinessLayer.DTOs.Beverages;

namespace BussinessLayer.Services.Abstraction
{
    public interface IBeverageCategoryService
    {
        Task<IEnumerable<BeverageCategoryDTO>> GetAllBeverageCategories();
    }
}