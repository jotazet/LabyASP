using Microsoft.AspNetCore.Mvc;
using Lab0.Models;

namespace Lab0.Controllers
{
    public class BirthController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new Birth());
        }

        [HttpPost]
        public IActionResult Index(Birth model)
        {
            if (!model.IsValid())
            {
                ViewBag.Error = "Podaj poprawne dane!";
                return View(model);
            }

            var age = model.CalculateAge();
            ViewBag.Message = $"Cześć {model.Name}, masz {age} lat(a).";
            return View(model);
        }
    }
}