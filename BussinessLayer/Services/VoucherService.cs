using BussinessLayer.Helper;
using BussinessLayer.Services.Abstraction;
using DataLayer.Entities;
using DataLayer.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Services
{
    public class VoucherService : IVoucherService
    {
        private readonly IGenericRepository<Voucher> _voucherRepository;

        public VoucherService(IGenericRepository<Voucher> voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }

        public async Task<IEnumerable<Voucher>> GetAllVouchersAsync()
        {
            return await _voucherRepository.GetAllAsync();
        }

        public async Task<Voucher> GetVoucherByIdAsync(string id)
        {
            return await _voucherRepository.GetAsync(v => v.Id == id);
        }

        public async Task CreateVoucherAsync(Voucher voucher)
        {
            if (string.IsNullOrEmpty(voucher.Code))
            {
                voucher.Code = await GenerateVoucherCodeAsync();
            }
            voucher.CreatedAt = TimeHelper.GetVietnamTime();
            voucher.UpdatedAt = TimeHelper.GetVietnamTime();

            await _voucherRepository.CreateAsync(voucher);
        }

        public async Task UpdateVoucherAsync(Voucher voucher)
        {
            var existing = await _voucherRepository.GetAsync(v => v.Id == voucher.Id);
            if (existing != null)
            {
                existing.Percentage = voucher.Percentage;
                existing.Description = voucher.Description;
                existing.MaxDiscountAmount = voucher.MaxDiscountAmount;
                existing.Amount = voucher.Amount;
                existing.StartDate = voucher.StartDate;
                existing.EndDate = voucher.EndDate;
                existing.UpdatedAt = TimeHelper.GetVietnamTime();

                await _voucherRepository.SaveAsync();
            }
        }

        public async Task DeleteVoucherAsync(string id)
        {
            var voucher = await _voucherRepository.GetAsync(v => v.Id == id);
            if (voucher != null)
            {
                await _voucherRepository.RemoveAsync(voucher);
            }
        }

        private async Task<string> GenerateVoucherCodeAsync()
        {
            string voucherCode = "V-" + Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();

            return voucherCode;
        }
		public async Task<Voucher?> GetVoucherByCodeAsync(string code)
		{
			return await _voucherRepository.GetAsync(v => v.Code == code);
		}

	}
}
