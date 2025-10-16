using AOWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AOWebApp.Controllers
{
    public class ReportsController : Controller
    {
        private readonly AmazonDbContext _context;

        public ReportsController(AmazonDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var YearList = _context.CustomerOrders.Select(i => i.OrderDate.Year).Distinct().OrderBy(i => i);
            return View("AnnualSalesReport", new SelectList(YearList));
        }

        [Produces("application/json")]
        public IActionResult AnnualSalesReportData(int? year)
        {
            if (!year.HasValue || year.Value <= 0)
            {
                return BadRequest();
            }

            var orderSummary = _context.ItemsInOrders
                .Where(iio => iio.OrderNumberNavigation.OrderDate.Year == year)
                .GroupBy(iio => new { iio.OrderNumberNavigation.OrderDate.Year, iio.OrderNumberNavigation.OrderDate.Month })
                .Select(group => new
                {
                    year = group.Key.Year,
                    monthNo = group.Key.Month,
                    monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(group.Key.Month),
                    totalItems = group.Sum(iio => iio.NumberOf),
                    totalSales = group.Sum(iio => iio.TotalItemCost)
                })
                .OrderBy(data => data.monthNo);

            return Json(orderSummary);
        }
    }
}