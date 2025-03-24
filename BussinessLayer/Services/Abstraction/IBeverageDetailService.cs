using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer.DTOs.Beverages;
using DataLayer.Entities;

namespace BussinessLayer.Services.Abstraction
{
	public interface IBeverageDetailService
	{
		Task CreateAsync(BeverageDetail beverageDetail);
		Task<IEnumerable<BeverageDetail>> GetAllBeverageDetailsAsync();
		Task<BeverageDetail?> GetByIdAsync(string id);
		Task<IEnumerable<BeverageDetail>> GetByBeverageIdAsync(string beverageId);
	}

}
