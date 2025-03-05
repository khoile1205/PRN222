using AutoMapper;
using BussinessLayer.DTOs.Beverages;
using BussinessLayer.Services.Abstraction;
using DataLayer.Entities;
using DataLayer.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services
{
    public class BeverageService : IBeverageService
    {
        private readonly IGenericRepository<Beverage> _beverageRepository;
        private readonly IGenericRepository<BeverageDetail> _beverageDetailRepository;
        private readonly IMapper _mapper;

        public BeverageService(
           IGenericRepository<Beverage> beverageRepository,
           IGenericRepository<BeverageDetail> beverageDetailRepository,
           IMapper mapper)
        {
            _beverageRepository = beverageRepository;
            _beverageDetailRepository = beverageDetailRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateBeverageDTO createBeverageDTO)
        {
            try
            {
                var existedBeverage = await _beverageRepository.GetAsync(
                    b => b.Name == createBeverageDTO.Name,
                    includes: b => b.Include(b => b.BeverageDetails).ThenInclude(bd => bd.Size)
                );

                if (existedBeverage == null)
                {
                    var beverage = _mapper.Map<Beverage>(createBeverageDTO);
                    await _beverageRepository.CreateAsync(beverage);

                    var beverageDetail = _mapper.Map<BeverageDetail>(createBeverageDTO);
                    beverageDetail.BeverageId = beverage.Id;
                    await _beverageDetailRepository.CreateAsync(beverageDetail);
                }
                else
                {
                    var existedBeverageSize = existedBeverage.BeverageDetails
                        .FirstOrDefault(bd => bd.SizeId == createBeverageDTO.SizeId);

                    if (existedBeverageSize == null)
                    {
                        var beverageDetail = _mapper.Map<BeverageDetail>(createBeverageDTO);
                        beverageDetail.BeverageId = existedBeverage.Id;
                        await _beverageDetailRepository.CreateAsync(beverageDetail);
                    }
                    else
                    {
                        throw new InvalidOperationException("Beverage already exists with the same size.");
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception($"Validation Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while creating the beverage.", ex);
            }

        }

        public async Task<bool> DeleteBeverage(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) return false;
                var beverage = await _beverageRepository.GetAsync(b => b.Id == id);

                if (beverage == null)
                {
                    throw new Exception("Beverage not found.");
                }

                var beverageDetails = await _beverageDetailRepository.GetAllAsync(bd => bd.BeverageId == id);

                foreach (var beverageDetail in beverageDetails)
                {
                    await _beverageDetailRepository.RemoveAsync(beverageDetail);
                }

                await _beverageRepository.RemoveAsync(beverage);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Beverage>> GetAllBeverages()
        {
            return await _beverageRepository.GetAllAsync(
                includes:
                    b => b.Include(b => b.BeverageDetails)
                    .ThenInclude(bd => bd.Size)
                    .Include(b => b.BeverageCategory)
            );
        }

        public async Task UpdateBeverage(UpdateBeverageDTO updateBeverageDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(updateBeverageDTO.Id))
                {
                    throw new ArgumentException("Beverage ID is required.");
                }

                var beverage = await _beverageRepository.GetAsync(b => b.Id == updateBeverageDTO.Id);
                if (beverage == null)
                {
                    throw new KeyNotFoundException("Beverage not found.");
                }

                beverage.Name = updateBeverageDTO.Name;
                beverage.Image = updateBeverageDTO.ImageUrl;
                beverage.Description = updateBeverageDTO.Description;
                beverage.UpdatedAt = DateTime.Now;

                await _beverageRepository.UpdateAsync(beverage);

                var beverageDetail = await _beverageDetailRepository.GetAsync(bd => bd.BeverageId == updateBeverageDTO.Id);
                if (beverageDetail == null)
                {
                    beverageDetail = new BeverageDetail
                    {
                        Id = Guid.NewGuid().ToString(),
                        BeverageId = updateBeverageDTO.Id,
                        SizeId = updateBeverageDTO.SizeId ?? string.Empty,
                        Price = updateBeverageDTO.Price,
                        CreatedAt = DateTime.Now
                    };

                    await _beverageDetailRepository.CreateAsync(beverageDetail);
                }
                else
                {
                    beverageDetail.SizeId = updateBeverageDTO.SizeId ?? string.Empty;
                    beverageDetail.Price = updateBeverageDTO.Price;
                    beverageDetail.UpdatedAt = DateTime.Now;

                    await _beverageDetailRepository.UpdateAsync(beverageDetail);
                }
            }
            catch (ArgumentException ex)
            {
                throw new Exception($"Validation Error: {ex.Message}");
            }
            catch (KeyNotFoundException ex)
            {
                throw new Exception($"Not Found: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the beverage.", ex);
            }
        }
    }
}