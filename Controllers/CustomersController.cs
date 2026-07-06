using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projectbridgemvc.Models;
using System.Text;

namespace projectbridgemvc.Controllers
{
    public class CustomersController : Controller
    {
        private readonly string apiUrl = "https://localhost:7115/api/Customers";

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Customer> customers = new List<Customer>();

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(apiUrl + "/GetCustomers");

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();

                    customers = JsonConvert.DeserializeObject<List<Customer>>(jsonData)
                                ?? new List<Customer>();
                }
            }

            return View(customers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Customer());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(customer),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await client.PostAsync(apiUrl + "/AddCustomer", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(customer);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Customer customer = new Customer();

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(apiUrl + "/GetCustomerById/" + id);

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();

                    customer = JsonConvert.DeserializeObject<Customer>(jsonData);
                }
            }

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Customer customer)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(customer),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await client.PutAsync(
                    apiUrl + "/UpdateCustomer/" + customer.CustomerId,
                    content
                );

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(customer);
        }

        public async Task<IActionResult> Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                await client.DeleteAsync(apiUrl + "/DeleteCustomer/" + id);
            }

            return RedirectToAction("Index");
        }
    }
}