using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projectbridgemvc.Models;
using System.Text;

namespace projectbridgemvc.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly string apiUrl = "https://localhost:7115/api/Suppliers";

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Supplier> suppliers = new List<Supplier>();

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(apiUrl + "/GetSuppliers");

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();

                    suppliers = JsonConvert.DeserializeObject<List<Supplier>>(jsonData)
                                ?? new List<Supplier>();
                }
            }

            return View(suppliers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Supplier());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Supplier supplier)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(supplier),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await client.PostAsync(apiUrl + "/AddSupplier", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(supplier);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Supplier supplier = new Supplier();

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(apiUrl + "/GetSupplierById/" + id);

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();

                    supplier = JsonConvert.DeserializeObject<Supplier>(jsonData);
                }
            }

            return View(supplier);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Supplier supplier)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(supplier),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await client.PutAsync(
                    apiUrl + "/UpdateSupplier/" + supplier.SupplierId,
                    content
                );

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(supplier);
        }

        public async Task<IActionResult> Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                await client.DeleteAsync(apiUrl + "/DeleteSupplier/" + id);
            }

            return RedirectToAction("Index");
        }
    }
}