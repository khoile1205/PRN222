using DataLayer.Entities;
using DataLayer.Repositories.Abstraction;
using BussinessLayer.Services.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Services
{
    public class InventoryCategoryService : IInventoryCategoryService
    {
        private readonly IInventoryCategoryRepository _categoryRepository;

        public InventoryCategoryService(IInventoryCategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<InventoryCategory> CreateInventoryCategoryAsync(InventoryCategory category)
        {
            try
            {
                category.UpdatedAt = DateTime.Now;
                await _categoryRepository.CreateAsync(category);
                return category;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating inventory category: {ex.Message}", ex);
            }
        }

        public async Task<InventoryCategory> GetInventoryCategoryByIdAsync(string id)
        {
            try
            {
                return await _categoryRepository.GetAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching inventory category: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<InventoryCategory>> GetAllInventoryCategoriesAsync()
        {
            try
            {
                return await _categoryRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching inventory categories: {ex.Message}", ex);
            }
        }

        public async Task UpdateInventoryCategoryAsync(InventoryCategory category)
        {
            try
            {
                category.UpdatedAt = DateTime.Now;
                await _categoryRepository.UpdateAsync(category);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating inventory category: {ex.Message}", ex);
            }
        }

        public async Task DeleteInventoryCategoryAsync(string id)
        {
            try
            {
                var category = await GetInventoryCategoryByIdAsync(id);
                if (category != null)
                    await _categoryRepository.RemoveAsync(category);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting inventory category: {ex.Message}", ex);
            }
        }
    }
}