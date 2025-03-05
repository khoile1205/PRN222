using AutoMapper;
using BussinessLayer.DTOs.Beverages;
using BussinessLayer.Services.Abstraction;
using DataLayer.Entities;
using DataLayer.Repositories.Abstraction;

namespace BussinessLayer.Services
{
    public class BeverageSizeService : IBeverageSizeService
    {
        private readonly IGenericRepository<BeverageSize> _beverageSizeRepository;
        private readonly IMapper _mapper;

        public BeverageSizeService(
           IGenericRepository<BeverageSize> beverageSizeRepository,
           IMapper mapper)
        {
            _beverageSizeRepository = beverageSizeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BeverageSizeDTO>> GetAllBeverageSize()
        {
            var beverageSize = await _beverageSizeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BeverageSizeDTO>>(beverageSize);
        }
    }
}