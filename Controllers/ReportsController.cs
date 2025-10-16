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
            if (!year.HasValue || year.Value <= 0) {
                return BadRequest();
            }

            var matches = _context.ItemsInOrders.Where(i => i.OrderNumberNavigation.OrderDate.Year == year)
            .GroupBy(i => i.OrderNumberNavigation.OrderDate.Month)
            .Select(i => new
            {
                monthName = System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(i.Key),
                Month = i.Key,
                Year = year,
                totalItems = i.Sum(i => i.NumberOf),
                totalSales = i.Sum(i => i.TotalItemCost)
            }).OrderBy(i => i.Month).AsEnumerable();
            
            return Json(matches);
        }
    }
}
