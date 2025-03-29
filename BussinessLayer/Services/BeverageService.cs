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
        private readonly ApplicationDbContext _context;
        private readonly IGenericRepository<Beverage> _beverageRepository;
        private readonly IGenericRepository<BeverageDetail> _beverageDetailRepository;
        private readonly IMapper _mapper;

        public BeverageService(
           IGenericRepository<Beverage> beverageRepository,
           IGenericRepository<BeverageDetail> beverageDetailRepository,
           ApplicationDbContext context,
           IMapper mapper)
        {
            _beverageRepository = beverageRepository;
            _beverageDetailRepository = beverageDetailRepository;
            _mapper = mapper;
            _context = context;
        }

        public async Task CreateAsync(CreateBeverageDTO createBeverageDTO)
        {
            var strategy = _context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
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
                    }
                    else
                    {
                        var existedSizeMap = existedBeverage.BeverageDetails.ToDictionary(d => d.SizeId, d => d.Size.SizeName);
                        var duplicateSizeMap = createBeverageDTO.Details
                            .Where(d => existedSizeMap.ContainsKey(d.SizeId))
                            .Select(d => existedSizeMap[d.SizeId])
                            .ToList();

                        if (duplicateSizeMap.Any())
                        {
                            throw new InvalidOperationException($"Beverage already exists with sizes: {string.Join(", ", duplicateSizeMap)}.");
                        }

                        foreach (var detailDTO in createBeverageDTO.Details)
                        {
                            var beverageDetail = _mapper.Map<BeverageDetail>(detailDTO);
                            beverageDetail.BeverageId = existedBeverage.Id;
                            await _beverageDetailRepository.CreateAsync(beverageDetail);
                        }
                    }

                    await transaction.CommitAsync();
                }
                catch (InvalidOperationException ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"Validation Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("An unexpected error occurred while creating the beverage.", ex);
                }
            });
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

        public async Task<Beverage> GetBeverageById(string beverageId)
        {
            return await _beverageRepository.GetAsync(s => s.Id == beverageId,
                includes:
                    b => b.Include(b => b.BeverageDetails)
                    .ThenInclude(bd => bd.Size)
                    .Include(b => b.BeverageCategory)
            );
        }

        public async Task UpdateBeverage(UpdateBeverageDTO updateBeverageDTO)
        {
            var strategy = _context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    if (string.IsNullOrEmpty(updateBeverageDTO.Id))
                    {
                        throw new ArgumentException("Beverage ID is required.");
                    }

                    if (updateBeverageDTO.Details == null || !updateBeverageDTO.Details.Any())
                    {
                        throw new ArgumentException("At least one size and price is required.");
                    }

                    var beverage = await _beverageRepository.GetAsync(
                        b => b.Id == updateBeverageDTO.Id,
                        includes: b => b.Include(b => b.BeverageDetails)
                    );
                    if (beverage == null)
                    {
                        throw new KeyNotFoundException("Beverage not found.");
                    }

                    beverage.Name = updateBeverageDTO.Name;
                    beverage.CategoryId = updateBeverageDTO.CategoryId;
                    beverage.Image = updateBeverageDTO.ImageUrl;
                    beverage.Description = updateBeverageDTO.Description;
                    beverage.UpdatedAt = DateTime.UtcNow;

                    var existingDetails = beverage.BeverageDetails.ToList();
                    foreach (var detailDto in updateBeverageDTO.Details)
                    {
                        var existingDetail = existingDetails.FirstOrDefault(d => d.SizeId == detailDto.SizeId);
                        if (existingDetail != null)
                        {
                            existingDetail.Price = detailDto.Price;
                            await _beverageDetailRepository.UpdateAsync(existingDetail);
                        }
                        else
                        {
                            var newDetail = _mapper.Map<BeverageDetail>(detailDto);
                            newDetail.BeverageId = beverage.Id;
                            await _beverageDetailRepository.CreateAsync(newDetail);
                            beverage.BeverageDetails.Add(newDetail);
                        }
                    }

                    foreach (var existingDetail in existingDetails)
                    {
                        if (!updateBeverageDTO.Details.Any(d => d.SizeId == existingDetail.SizeId))
                        {
                            await _beverageDetailRepository.RemoveAsync(existingDetail);
                        }
                    }

                    await _beverageRepository.UpdateAsync(beverage);
                    await transaction.CommitAsync();
                }
                catch (ArgumentException ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"Validation Error: {ex.Message}");
                }
                catch (KeyNotFoundException ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"Not Found: {ex.Message}");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("An unexpected error occurred while updating the beverage.", ex);
                }
            });
        }
    }
}