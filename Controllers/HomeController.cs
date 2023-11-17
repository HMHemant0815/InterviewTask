using Microsoft.AspNetCore.Mvc;
using PracticalTask1.Models;
using System.Diagnostics;
using System.Net.Http;

namespace PracticalTask1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await  _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/users/");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<DashboardDetail>>();

                return View(result);
            }
            return View(null);
        }

        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/users/{id}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<DashboardDetail>();

                return View(result);
            }
            return View(null);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    
    }
}
