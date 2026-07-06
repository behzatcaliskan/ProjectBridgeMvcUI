using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projectbridgemvc.Models;

namespace projectbridgemvc.Controllers
{
    public class DevelopersController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Developers> developers = new List<Developers>();

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("https://localhost:7115/api/Developers/GetDevelopers");

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();

                    developers = JsonConvert.DeserializeObject<List<Developers>>(jsonData)
                                 ?? new List<Developers>();
                }
            }

            return View(developers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Developers());
        }

        [HttpPost]
        public IActionResult Create(Developers developers)
        {
            HttpClient client = new HttpClient();

            StringContent content = new StringContent(
                JsonConvert.SerializeObject(developers),
                System.Text.Encoding.UTF8,
                "application/json"
            );

            var response = client.PostAsync(
                "https://localhost:7115/api/Developers/AddDevelopers",
                content
            ).Result;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            HttpClient client = new HttpClient();

            var response = client.GetAsync(
                "https://localhost:7115/api/Developers/GetDevelopersById/" + id
            ).Result;

            if (response.IsSuccessStatusCode)
            {
                var jsonData = response.Content.ReadAsStringAsync().Result;

                var developer = JsonConvert.DeserializeObject<Developers>(jsonData);

                return View(developer);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(Developers developers)
        {
            HttpClient client = new HttpClient();

            StringContent content = new StringContent(
                JsonConvert.SerializeObject(developers),
                System.Text.Encoding.UTF8,
                "application/json"
            );

            var response = client.PutAsync(
                "https://localhost:7115/api/Developers/UpdateDevelopers/" + developers.DevelopersId,
                content
            ).Result;

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            HttpClient client = new HttpClient();

            client.DeleteAsync($"https://localhost:7115/api/Developers/DeleteDeveloper/{id}").Wait();

            return RedirectToAction("Index");
        }
    }
}