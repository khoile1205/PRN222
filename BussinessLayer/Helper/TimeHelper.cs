using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Helper
{
	public static class TimeHelper
	{
		private static readonly TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

		public static DateTime GetVietnamTime()
		{
			return TimeZoneInfo.ConvertTime(DateTime.UtcNow, vnTimeZone);
		}
	}
}
