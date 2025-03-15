using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;

namespace BussinessLayer.Services.Abstraction
{
	public interface ITableDetailService
	{
		Task<IEnumerable<TableDetail>> GetAllTableDetailsAsync();
		Task<TableDetail?> GetTableDetaileByIdAsync(string id);
		Task CreateAsync(TableDetail tableDetail);
		Task UpdateAsync(TableDetail tableDetail);

	}
}
