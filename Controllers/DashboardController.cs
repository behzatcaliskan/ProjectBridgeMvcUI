using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectbridgemvc.Models;

namespace projectbridgemvc.Controllers
{
    public class DashboardController : Controller
    {
        private readonly MvcDbContext context;

        public DashboardController(MvcDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var role = HttpContext.Session.GetString("Role");

            if (string.IsNullOrEmpty(role))
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.TotalProductCount = await context.Products.CountAsync();
            ViewBag.TotalSaleCount = await context.Sales.CountAsync();
            ViewBag.TotalRevenue = await context.Sales.SumAsync(x => x.TotalPrice);
            ViewBag.LowStockCount = await context.Products.CountAsync(x => x.Stock <= 5);

            ViewBag.LastSales = await context.Sales
                .Include(x => x.Product)
                .OrderByDescending(x => x.SaleDate)
                .Take(5)
                .ToListAsync();

            return View();
        }
    }
}