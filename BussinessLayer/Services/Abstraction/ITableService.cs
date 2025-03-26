using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;

namespace BussinessLayer.Services.Abstraction
{
    public interface ITableService
    {
        Task<IEnumerable<Table>> GetAllTablesAsync();
        Task<Table?> GetTableByIdAsync(string id);
        Task CreateTableAsync(Table table);
        Task UpdateTableAsync(Table table);
        Task<bool> DeleteTableAsync(string id);
        Task<IEnumerable<Table>> GetAvailableTablesAsync();
        Task<bool> RestoreTableAsync(string id);

    }

}
