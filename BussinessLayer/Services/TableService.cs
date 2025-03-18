using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer.Services.Abstraction;
using DataLayer.Entities;
using DataLayer.Repositories.Abstraction;

namespace BussinessLayer.Services
{
    public class TableService : ITableService
    {
        private readonly IGenericRepository<Table> _tableRepository;

        public TableService(IGenericRepository<Table> tableRepository)
        {
            _tableRepository = tableRepository;
        }

        public async Task<IEnumerable<Table>> GetAllTablesAsync()
        {
            return await _tableRepository.GetAllAsync();
        }

        public async Task<Table?> GetTableByIdAsync(string id)
        {
            return await _tableRepository.GetAsync(t => t.Id == id);
        }

        public async Task<bool> CreateTableAsync(Table table)
        {
            await _tableRepository.CreateAsync(table);
            return true;
        }

        public async Task<bool> UpdateTableAsync(Table table)
        {
            await _tableRepository.UpdateAsync(table);
            return true;
        }

        public async Task<bool> DeleteTableAsync(string id)
        {
            var table = await _tableRepository.GetAsync(t => t.Id == id);
            if (table == null) return false;

            await _tableRepository.RemoveAsync(table);
            return true;
        }
    }
}