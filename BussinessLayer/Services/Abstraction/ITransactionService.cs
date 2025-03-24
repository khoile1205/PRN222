using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;

namespace BussinessLayer.Services.Abstraction
{
    public interface ITransactionService
    {
        Task<bool> CreateTransactionAsync(Transaction transaction);
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync(DateTime? startDate, DateTime? endDate, int skip, int take);
        Task<Transaction?> GetTransactionByIdAsync(string id);
    }

}
