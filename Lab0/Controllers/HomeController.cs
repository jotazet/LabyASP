using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Lab0.Models;

namespace Lab0.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();    
    }
    // Akcja i widok Calculator

    public IActionResult Age(string date)
    {
        DateTime birthDate;
        if (!DateTime.TryParse(date, out birthDate))
        {
            ViewBag.Error = "Invalid date format";
            return View();
        }

        int currentYear = DateTime.Now.Year;
        int age = currentYear - birthDate.Year;

        if (DateTime.Now < birthDate.AddYears(age))
        {
            age--;
        }

        ViewBag.Age = age;
        return View();
    }
    public IActionResult Calculator(double? x, double? y, [FromQuery(Name="operator-val")] string op)
    {
        // Gdy brak parametrów w query (pierwsze wejście na stronę) — pokaż formularz
        if (x is null || y is null)
        {
            return View();
        }

        switch (op)
        {
            case "add":
                ViewBag.Result = $"{x} + {y} = {x + y}";
                break;
            case "sub":
                ViewBag.Result = $"{x} - {y} = {x - y}";
                break;
            case "mul":
                ViewBag.Result = $"{x} * {y} = {x * y}";
                break;
            case "div":
                ViewBag.Result = y == 0 ? "Dzielenie przez zero!" : $"{x} / {y} = {x / y}";
                break;
            default:
                ViewBag.Result = "Nieznany operator";
                break;
        }

        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}