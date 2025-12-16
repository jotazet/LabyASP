using Lab0.Models;
using Microsoft.AspNetCore.Mvc;
using Lab0.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Lab0.Controllers;

[Authorize]
public class CarController : Controller
{
    private readonly ICarService _cars;
    private readonly ICompanyService _companies;

    public CarController(ICarService cars, ICompanyService companies)
    {
        _cars = cars;
        _companies = companies;
    }

    private async Task PopulateCompaniesAsync(int? selectedId = null)
    {
        var list = await _companies.GetAllAsync();
        ViewBag.Companies = new SelectList(list, nameof(Company.Id), nameof(Company.Name), selectedId);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var cars = await _cars.GetAllAsync();
        ViewData[LastVisitCookie.CookieName] = Response.HttpContext.Items[LastVisitCookie.CookieName];
        return View(cars.ToList());
    }

    public async Task<IActionResult> Create()
    {
        await PopulateCompaniesAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Car car)
    {
        if (!ModelState.IsValid)
        {
            await PopulateCompaniesAsync(car.CompanyId);
            return View(car);
        }
        await _cars.AddAsync(car);
        return RedirectToAction(nameof(Index));
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(string registrationNumber)
    {
        var car = await _cars.GetByRegistrationAsync(registrationNumber);
        if (car == null)
        {
            return NotFound();
        }
        return View(car);
    }

    public async Task<IActionResult> Edit(string registrationNumber)
    {
        var car = await _cars.GetByRegistrationAsync(registrationNumber);
        if (car == null)
        {
            return NotFound();
        }
        await PopulateCompaniesAsync(car.CompanyId);
        return View(car);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string registrationNumber, Car updated)
    {
        if (!ModelState.IsValid)
        {
            await PopulateCompaniesAsync(updated.CompanyId);
            return View(updated);
        }
        var existing = await _cars.GetByRegistrationAsync(registrationNumber);
        if (existing == null)
        {
            return NotFound();
        }
        // Preserve identity and key values
        updated.Id = existing.Id;
        updated.RegistrationNumber = existing.RegistrationNumber;
        await _cars.UpdateAsync(updated);
        return RedirectToAction(nameof(Details), new { registrationNumber });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(string registrationNumber)
    {
        await _cars.DeleteByRegistrationAsync(registrationNumber);
        return RedirectToAction(nameof(Index));
    }
}