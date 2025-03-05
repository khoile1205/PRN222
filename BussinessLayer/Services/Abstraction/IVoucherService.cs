using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Services.Abstraction
{
    public interface IVoucherService
    {
        public Task<IEnumerable<Voucher>> GetAllVouchersAsync();
        public Task<Voucher> GetVoucherByIdAsync(string id);
        public Task CreateVoucherAsync(Voucher voucher);
        public Task UpdateVoucherAsync(Voucher voucher);
        public Task DeleteVoucherAsync(string id);
    }
}
