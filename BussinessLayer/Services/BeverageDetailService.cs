using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer.Services.Abstraction;
using DataLayer.Entities;
using DataLayer.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services
{
	public class BeverageDetailService : IBeverageDetailService
	{
		private readonly IGenericRepository<BeverageDetail> _beverageDetailRepository;

		public BeverageDetailService(IGenericRepository<BeverageDetail> beverageDetailRepository)
		{
			_beverageDetailRepository = beverageDetailRepository;
		}

		public async Task CreateAsync(BeverageDetail beverageDetail)
		{
			await _beverageDetailRepository.CreateAsync(beverageDetail);
		}

		public async Task<IEnumerable<BeverageDetail>> GetAllBeverageDetailsAsync()
		{
			var beverageDetails = await _beverageDetailRepository
				.GetAllAsync(includes: query => query
				.Include(b => b.Beverage)
				.ThenInclude(b => b.BeverageCategory)
				.Include(b => b.Size)
		);

			return beverageDetails.OrderBy(b => b.Beverage.Name);

		}

		public async Task<BeverageDetail?> GetByIdAsync(string id)
		{
			return await _beverageDetailRepository.GetAsync(b => b.Id == id, includes: query => query.Include(b => b.Beverage).Include(b => b.Size));
		}

		public async Task<IEnumerable<BeverageDetail>> GetByBeverageIdAsync(string beverageId)
		{
			return await _beverageDetailRepository.GetAllAsync(b => b.BeverageId == beverageId, includes: query => query.Include(b => b.Size));
		}
	}

}
