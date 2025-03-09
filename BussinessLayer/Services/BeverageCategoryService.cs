using AutoMapper;
using BussinessLayer.DTOs.Beverages;
using BussinessLayer.Services.Abstraction;
using DataLayer.Entities;
using DataLayer.Repositories.Abstraction;

namespace BussinessLayer.Services
{
    public class BeverageCategoryService : IBeverageCategoryService
    {
        private readonly IGenericRepository<BeverageCategory> _beverageCategoryRepository;
        private readonly IMapper _mapper;

        public BeverageCategoryService(
           IGenericRepository<BeverageCategory> beverageCategoryRepository,
           IMapper mapper)
        {
            _beverageCategoryRepository = beverageCategoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BeverageCategoryDTO>> GetAllBeverageCategories()
        {
            var beverageCategories = await _beverageCategoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BeverageCategoryDTO>>(beverageCategories);
        }
    }
}