using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer.Helper;
using BussinessLayer.Services.Abstraction;
using DataLayer.Entities;
using DataLayer.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services
{
    public class TableService : ITableService
    {
        private readonly IGenericRepository<Table> _tableRepository;

        public TableService(IGenericRepository<Table> tableRepository)
        {
            _tableRepository = tableRepository;
        }

        public async Task CreateTableAsync(Table table)
        {
            table.CreatedAt = TimeHelper.GetVietnamTime();
            table.UpdatedAt = TimeHelper.GetVietnamTime();

            await _tableRepository.CreateAsync(table);
        }

        public async Task<bool> DeleteTableAsync(string id)
        {
            var table = await _tableRepository.GetAsync(
                filter: t => t.Id == id,
                includes: query => query.Include(t => t.TableDetails)
            );

            if (table == null) return false;

            if (table.TableDetails == null || !table.TableDetails.Any())
            {
                await _tableRepository.RemoveAsync(table);
                return true;
            }
            else
            {
                table.DeletedAt = TimeHelper.GetVietnamTime();
                await _tableRepository.UpdateAsync(table);
                return false;
            }
        }


        public async Task<IEnumerable<Table>> GetAllTablesAsync()
        {
            return await _tableRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Table>> GetAvailableTablesAsync()
        {
            return await _tableRepository.GetAllAsync(t => t.DeletedAt == null);
        }

        public async Task<Table?> GetTableByIdAsync(string id)
        {
            return await _tableRepository.GetAsync(t => t.Id == id);
        }

        public async Task UpdateTableAsync(Table table)
        {
            var tableExist = await _tableRepository.GetAsync(t => t.Id == table.Id);
            if (tableExist != null)
            {
                tableExist.TableName = table.TableName;
                tableExist.SeatQuantity = table.SeatQuantity;
                tableExist.Area = table.Area;
                tableExist.UpdatedAt = TimeHelper.GetVietnamTime();

                await _tableRepository.UpdateAsync(tableExist);
            }
        }

        public async Task<bool> RestoreTableAsync(string id)
        {
            var table = await _tableRepository.GetAsync(t => t.Id == id);
            if (table == null) return false;

            if (table.DeletedAt != null)
            {
                table.UpdatedAt = TimeHelper.GetVietnamTime();
                table.DeletedAt = null;
                await _tableRepository.UpdateAsync(table);
                return true;
            }

            return false;
        }

    }
}