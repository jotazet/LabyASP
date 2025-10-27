using Lab0.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab0.Controllers;

public class CarController : Controller
{
    private static readonly List<Car> Cars = new();

    public IActionResult Index()
    {
        return View(Cars);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Car car)
    {
        if (ModelState.IsValid)
        {
            Cars.Add(car);
            return RedirectToAction(nameof(Index));
        }
        return View(car);
    }

    public IActionResult Details(string registrationNumber)
    {
        var car = Cars.FirstOrDefault(c => c.RegistrationNumber == registrationNumber);
        if (car == null)
        {
            return NotFound();
        }
        return View(car);
    }
}