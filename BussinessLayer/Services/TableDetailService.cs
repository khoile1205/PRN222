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
	public class TableDetailService : ITableDetailService
	{
		private readonly IGenericRepository<TableDetail> _tableDetailRepository;

		public TableDetailService(IGenericRepository<TableDetail> tableDetailRepository)
		{
			_tableDetailRepository = tableDetailRepository;
		}

		public async Task<IEnumerable<TableDetail>> GetAllTableDetailsAsync()
		{
			return await _tableDetailRepository.GetAllAsync();
		}

		public async Task<TableDetail?> GetTableDetaileByIdAsync(string id)
		{
			return await _tableDetailRepository.GetAsync(t => t.Id == id);
		}

		public async Task CreateAsync(TableDetail tableDetail)
		{
			tableDetail.CreatedAt = TimeHelper.GetVietnamTime();
			tableDetail.UpdatedAt = TimeHelper.GetVietnamTime();
			await _tableDetailRepository.CreateAsync(tableDetail);
		}

		public async Task UpdateAsync(TableDetail tableDetail)
		{
			await _tableDetailRepository.UpdateAsync(tableDetail);
		}
	}

}
