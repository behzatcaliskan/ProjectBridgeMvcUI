using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projectbridgemvc.Models;

namespace projectbridgemvc.Controllers
{
    public class SalesController : Controller
    {
        private readonly MvcDbContext context;

        public SalesController(MvcDbContext context)
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

            var sales = await context.Sales
                .Include(x => x.Product)
                .ToListAsync();

            return View(sales);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Index");
            }

            ViewBag.Products = new SelectList(context.Products.ToList(), "ProductId", "ProductName");

            return View(new Sale
            {
                SaleDate = DateTime.Now
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(Sale sale)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Index");
            }

            var product = await context.Products.FindAsync(sale.ProductId);

            if (product == null)
            {
                ModelState.AddModelError("", "Ürün bulunamadı.");
                ViewBag.Products = new SelectList(context.Products.ToList(), "ProductId", "ProductName");
                return View(sale);
            }

            sale.TotalPrice = product.Price * sale.Quantity;

            if (sale.SaleDate == DateTime.MinValue)
            {
                sale.SaleDate = DateTime.Now;
            }

            await context.Sales.AddAsync(sale);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Index");
            }

            var sale = await context.Sales.FindAsync(id);

            if (sale == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Products = new SelectList(
                context.Products.ToList(),
                "ProductId",
                "ProductName",
                sale.ProductId
            );

            return View(sale);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Sale sale)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Index");
            }

            var product = await context.Products.FindAsync(sale.ProductId);

            if (product == null)
            {
                ModelState.AddModelError("", "Ürün bulunamadı.");

                ViewBag.Products = new SelectList(
                    context.Products.ToList(),
                    "ProductId",
                    "ProductName",
                    sale.ProductId
                );

                return View(sale);
            }

            sale.TotalPrice = product.Price * sale.Quantity;

            context.Sales.Update(sale);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Index");
            }

            var sale = await context.Sales.FindAsync(id);

            if (sale != null)
            {
                context.Sales.Remove(sale);
                await context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}