using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using projectbridgemvc.Models;

namespace projectbridgemvc.Controllers
{
    public class ReportsController : Controller
    {
        private readonly MvcDbContext context;

        public ReportsController(MvcDbContext context)
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

            if (role != "Admin")
            {
                return RedirectToAction("Index", "Products");
            }

            ViewBag.TotalProductCount = await context.Products.CountAsync();

            ViewBag.TotalSaleCount = await context.Sales.CountAsync();

            ViewBag.TotalRevenue = await context.Sales.SumAsync(x => x.TotalPrice);

            ViewBag.LowStockProducts = await context.Products
                .Where(x => x.Stock <= 5)
                .ToListAsync();

            var productSalesReport = await context.Sales
                .Include(x => x.Product)
                .GroupBy(x => x.Product.ProductName)
                .Select(x => new ReportProductSaleViewModel
                {
                    ProductName = x.Key,
                    TotalQuantity = x.Sum(y => y.Quantity),
                    TotalRevenue = x.Sum(y => y.TotalPrice)
                })
                .ToListAsync();

            ViewBag.ProductSalesReport = productSalesReport;

            using (HttpClient client = new HttpClient())
            {
                var categoryResponse = await client.GetAsync("https://localhost:7115/api/Categories/GetCategories");

                if (categoryResponse.IsSuccessStatusCode)
                {
                    var jsonData = await categoryResponse.Content.ReadAsStringAsync();
                    var categories = JsonConvert.DeserializeObject<List<Category>>(jsonData);
                    ViewBag.CategoryCount = categories?.Count ?? 0;
                }
                else
                {
                    ViewBag.CategoryCount = 0;
                }

                var supplierResponse = await client.GetAsync("https://localhost:7115/api/Suppliers/GetSuppliers");

                if (supplierResponse.IsSuccessStatusCode)
                {
                    var jsonData = await supplierResponse.Content.ReadAsStringAsync();
                    var suppliers = JsonConvert.DeserializeObject<List<Supplier>>(jsonData);
                    ViewBag.SupplierCount = suppliers?.Count ?? 0;
                }
                else
                {
                    ViewBag.SupplierCount = 0;
                }

                var customerResponse = await client.GetAsync("https://localhost:7115/api/Customers/GetCustomers");

                if (customerResponse.IsSuccessStatusCode)
                {
                    var jsonData = await customerResponse.Content.ReadAsStringAsync();
                    var customers = JsonConvert.DeserializeObject<List<Customer>>(jsonData);
                    ViewBag.CustomerCount = customers?.Count ?? 0;
                }
                else
                {
                    ViewBag.CustomerCount = 0;
                }
            }

            return View();
        }
    }
}