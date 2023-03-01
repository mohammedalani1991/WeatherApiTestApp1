using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;
using AppLoger;
using WeatherApiNuget;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static WebApplication1.Models.Class;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IWeatherApi _weather;

        public HomeController(ILogger<HomeController> logger, IWeatherApi weather)

        {
            _logger = logger;
            _weather = weather;
        }

       
        public async Task<object> Getweather(string cityName,bool isCelius)
        {
            var Temp =await _weather.GetCurrentWeather(cityName, isCelius);
            Temp = (isCelius ? Temp + " C" : Temp + " F") +" in "+cityName;
            return View("Index", Temp);

        }
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://api.weatherapi.com/v1/current.json?key=d1398cba8a894feb9f7180821232602&q=London&aqi=no\r\n");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var logger = new Logger();
            logger.Log("sssss");
            return View();
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