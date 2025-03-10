using BussinessLayer.DTOs.Revenues;
using BussinessLayer.Helper;
using BussinessLayer.Services.Abstraction;
using DataLayer.Entities;
using DataLayer.Repositories.Abstraction;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BussinessLayer.Services
{
    public class RevenueService : IRevenueService
    {
        private readonly IGenericRepository<Transaction> _transactionRepository;

        public RevenueService(IGenericRepository<Transaction> transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<IEnumerable<RevenueDTO>> GetRevenues(RevenueRangeTypeEnum rangeType, DateTime? startTime, DateTime? endTime)
        {
            if (!startTime.HasValue || !endTime.HasValue)
            {
                (startTime, endTime) = GetDefaultDateRange(rangeType, startTime, endTime);
            }

            var transactions = await _transactionRepository.GetAllAsync(t => t.CreatedAt >= startTime && t.CreatedAt <= endTime);

            return GroupRevenueByRangeType(transactions, rangeType, startTime.Value, endTime.Value);
        }

        private (DateTime start, DateTime end) GetDefaultDateRange(RevenueRangeTypeEnum rangeType, DateTime? startTime, DateTime? endTime)
        {
            DateTime now = DateTime.UtcNow;
            DateTime defaultStart;
            DateTime defaultEnd = endTime ?? now;

            switch (rangeType)
            {
                case RevenueRangeTypeEnum.Day:
                    defaultStart = defaultEnd.AddDays(-7);
                    break;
                case RevenueRangeTypeEnum.Week:
                    defaultStart = defaultEnd.AddDays(-28);
                    break;
                case RevenueRangeTypeEnum.Month:
                    defaultStart = defaultEnd.AddMonths(-4);
                    break;
                case RevenueRangeTypeEnum.Quarter:
                    defaultStart = defaultEnd.AddMonths(-9);
                    break;
                case RevenueRangeTypeEnum.Year:
                    defaultStart = defaultEnd.AddYears(-3);
                    break;
                default:
                    defaultStart = defaultEnd.AddDays(-7);
                    break;
            }

            return (startTime ?? defaultStart.Date.AddHours(0).AddMinutes(0).AddSeconds(0), defaultEnd.Date.AddHours(23).AddMinutes(59).AddSeconds(59));
        }
        private IEnumerable<RevenueDTO> GroupRevenueByRangeType(IEnumerable<Transaction> transactions,
            RevenueRangeTypeEnum rangeType, DateTime startTime, DateTime endTime)
        {
            // Sort transactions by date for consistent processing
            var orderedTransactions = transactions.OrderBy(t => t.CreatedAt).ToList();

            switch (rangeType)
            {
                case RevenueRangeTypeEnum.Day:
                    return GroupByDay(orderedTransactions, startTime, endTime);
                case RevenueRangeTypeEnum.Week:
                    return GroupByWeek(orderedTransactions, startTime, endTime);
                case RevenueRangeTypeEnum.Month:
                    return GroupByMonth(orderedTransactions, startTime, endTime);
                case RevenueRangeTypeEnum.Quarter:
                    return GroupByQuarter(orderedTransactions, startTime, endTime);
                case RevenueRangeTypeEnum.Year:
                    return GroupByYear(orderedTransactions, startTime, endTime);
                default:
                    throw new ArgumentOutOfRangeException(nameof(rangeType), rangeType, null);
            }
        }

        private IEnumerable<RevenueDTO> GroupByDay(List<Transaction> transactions, DateTime startDate, DateTime endDate)
        {
            var result = new List<RevenueDTO>();

            // Create a lookup for faster searching
            var transactionsByDay = transactions
                .GroupBy(t => t.CreatedAt.Date)
                .ToDictionary(g => g.Key, g => g.Sum(t => t.Price));

            // Generate all dates in the range
            for (var date = startDate.Date; date <= endDate.Date; date = date.AddDays(1))
            {
                result.Add(new RevenueDTO
                {
                    Label = DateTimeHelper.FormatDayLabel(date),
                    Revenue = transactionsByDay.ContainsKey(date) ? transactionsByDay[date] : 0
                });
            }

            return result;
        }

        private IEnumerable<RevenueDTO> GroupByWeek(List<Transaction> transactions, DateTime startDate, DateTime endDate)
        {
            var result = new List<RevenueDTO>();

            // Find the first day of the week for the start date
            var currentDate = DateTimeHelper.GetStartOfWeek(startDate);

            // Create groups by week with labels showing full date ranges
            while (currentDate <= endDate)
            {
                var weekStart = currentDate;
                var weekEnd = weekStart.AddDays(6);

                var weekRevenue = transactions
                    .Where(t => t.CreatedAt.Date >= weekStart && t.CreatedAt.Date <= weekEnd)
                    .Sum(t => t.Price);

                result.Add(new RevenueDTO
                {
                    Label = DateTimeHelper.FormatWeekLabel(weekStart, weekEnd),
                    Revenue = weekRevenue
                });

                currentDate = weekEnd.AddDays(1);
            }

            return result;
        }

        private IEnumerable<RevenueDTO> GroupByMonth(List<Transaction> transactions, DateTime startDate, DateTime endDate)
        {
            var result = new List<RevenueDTO>();

            // Find the first day of the month for the start date
            var currentDate = DateTimeHelper.GetStartOfMonth(startDate);

            // Group by year and month
            var transactionsByMonth = transactions
                .GroupBy(t => new { t.CreatedAt.Year, t.CreatedAt.Month })
                .ToDictionary(g => g.Key, g => g.Sum(t => t.Price));

            // Generate all months in the range
            while (currentDate <= endDate)
            {
                var key = new { currentDate.Year, currentDate.Month };

                result.Add(new RevenueDTO
                {
                    Label = DateTimeHelper.FormatMonthLabel(currentDate),
                    Revenue = transactionsByMonth.ContainsKey(key) ? transactionsByMonth[key] : 0
                });

                currentDate = currentDate.AddMonths(1);
            }

            return result;
        }

        private IEnumerable<RevenueDTO> GroupByQuarter(List<Transaction> transactions, DateTime startDate, DateTime endDate)
        {
            var result = new List<RevenueDTO>();

            // Find the first day of the quarter for the start date
            var currentDate = DateTimeHelper.GetStartOfQuarter(startDate);

            // Group by year and quarter
            var transactionsByQuarter = transactions
                .GroupBy(t => new
                {
                    Year = t.CreatedAt.Year,
                    Quarter = DateTimeHelper.GetQuarter(t.CreatedAt)
                })
                .ToDictionary(g => g.Key, g => g.Sum(t => t.Price));

            // Generate all quarters in the range
            while (currentDate <= endDate)
            {
                int quarter = DateTimeHelper.GetQuarter(currentDate);
                var key = new { Year = currentDate.Year, Quarter = quarter };

                result.Add(new RevenueDTO
                {
                    Label = DateTimeHelper.FormatQuarterLabel(currentDate),
                    Revenue = transactionsByQuarter.ContainsKey(key) ? transactionsByQuarter[key] : 0
                });

                // Move to the next quarter
                currentDate = currentDate.AddMonths(3);
            }

            return result;
        }

        private IEnumerable<RevenueDTO> GroupByYear(List<Transaction> transactions, DateTime startDate, DateTime endDate)
        {
            var result = new List<RevenueDTO>();

            // Group by year
            var transactionsByYear = transactions
                .GroupBy(t => t.CreatedAt.Year)
                .ToDictionary(g => g.Key, g => g.Sum(t => t.Price));

            // Generate all years in the range
            for (int year = startDate.Year; year <= endDate.Year; year++)
            {
                result.Add(new RevenueDTO
                {
                    Label = year.ToString(),
                    Revenue = transactionsByYear.ContainsKey(year) ? transactionsByYear[year] : 0
                });
            }

            return result;
        }
    }
}