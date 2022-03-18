using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using coinpayments_test.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace coinpayments_test.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _configuration;

  public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
  {
    _logger = logger;
    _configuration = configuration;
  }

  public IActionResult Index()
    {
        ViewData["ClientUrl"] = _configuration["ClientUrl"];
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

    [HttpPost]
    public IActionResult Webhook()
    {
        _logger.LogInformation($"Received payload:");
        // _logger.LogInformation(payload.ToString());
        var dictionary = HttpContext.Request.Form.Keys.ToDictionary(s => s, s => HttpContext.Request.Form[s]);
        string json = JsonConvert.SerializeObject(dictionary, Formatting.Indented);
        _logger.LogInformation(json);
        
        // foreach(var key in HttpContext.Request.Form.Keys)
        // {
        //     var val = HttpContext.Request.Form[key];
        //     _logger.LogInformation($"{key}: {val}");

        //     //process the form data
        // }

        return Ok();
    }
}
