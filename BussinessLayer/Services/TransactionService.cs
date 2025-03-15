using System;
using System.Collections.Generic;
using BussinessLayer.Helper;
using BussinessLayer.Services.Abstraction;
using DataLayer.Entities;
using DataLayer.Enums;
using DataLayer.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BussinessLayer.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IGenericRepository<Transaction> _transactionRepository;
        private readonly IGenericRepository<DataLayer.Entities.Table> _tableDetailRepository;

        public TransactionService(
            IGenericRepository<Transaction> transactionRepository,
            IGenericRepository<DataLayer.Entities.Table> tableDetailRepository)
        {
            _transactionRepository = transactionRepository;
            _tableDetailRepository = tableDetailRepository;
        }

        public async Task<bool> CreateTransactionAsync(Transaction transaction)
        {
            transaction.CreatedAt = TimeHelper.GetVietnamTime();
            transaction.UpdatedAt = TimeHelper.GetVietnamTime();

            await _transactionRepository.CreateAsync(transaction);
            return true;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            var transactions = await _transactionRepository.GetAllAsync(
                includes: u => u.Include(u => u.TableDetail)
                                .ThenInclude(td => td.Table)
                            .Include(u => u.TableDetail.TableBeverages)
                                .ThenInclude(tb => tb.BeverageDetail)
                                    .ThenInclude(bd => bd.Beverage)
                            .Include(u => u.TableDetail.TableBeverages)
                                .ThenInclude(tb => tb.BeverageDetail)
                                    .ThenInclude(bd => bd.Size)
            );
            return transactions.OrderByDescending(t => t.CreatedAt);
        }


        public async Task<Transaction?> GetTransactionByIdAsync(string id)
        {
            return await _transactionRepository.GetAsync(
                t => t.Id == id,
                includes: query => query
                    .Include(t => t.TableDetail)
                        .ThenInclude(td => td.Table)
                    .Include(t => t.TableDetail)
                        .ThenInclude(td => td.TableBeverages)
                            .ThenInclude(tb => tb.BeverageDetail)
                                .ThenInclude(bd => bd.Beverage)
                    .Include(t => t.TableDetail)
                        .ThenInclude(td => td.TableBeverages)
                            .ThenInclude(tb => tb.BeverageDetail)
                            .ThenInclude(bd => bd.Size)
                    .Include(t => t.Voucher)
            );
        }


    }

}
