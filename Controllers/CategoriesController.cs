using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projectbridgemvc.Models;
using System.Text;

namespace projectbridgemvc.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly string apiUrl = "https://localhost:7115/api/Categories";

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Category> categories = new List<Category>();

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(apiUrl + "/GetCategories");

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();

                    categories = JsonConvert.DeserializeObject<List<Category>>(jsonData)
                                 ?? new List<Category>();
                }
            }

            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Category());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(category),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await client.PostAsync(apiUrl + "/AddCategory", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Category category = new Category();

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(apiUrl + "/GetCategoryById/" + id);

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();

                    category = JsonConvert.DeserializeObject<Category>(jsonData);
                }
            }

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(category),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await client.PutAsync(
                    apiUrl + "/UpdateCategory/" + category.CategoryId,
                    content
                );

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(category);
        }

        public async Task<IActionResult> Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.DeleteAsync(apiUrl + "/DeleteCategory/" + id);
            }

            return RedirectToAction("Index");
        }
    }
}