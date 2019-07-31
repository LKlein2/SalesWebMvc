using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMvcContext _context;

        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? initial, DateTime? final)
        {
            var result = from sales in _context.SalesRecord select sales;
            if (initial.HasValue)
            {
                result = result.Where(sale => sale.Date >= initial.Value);
            }
            if (final.HasValue)
            {
                result = result.Where(sale => sale.Date <= final.Value);
            }
            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? initial, DateTime? final)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (initial.HasValue)
            {
                result = result.Where(x => x.Date >= initial.Value);
            }
            if (final.HasValue)
            {
                result = result.Where(x => x.Date <= final.Value);
            }
            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .GroupBy(x => x.Seller.Department)
                .ToListAsync();
        }
    }
}
