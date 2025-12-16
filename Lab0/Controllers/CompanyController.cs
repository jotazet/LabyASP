using Lab0.Models;
using Lab0.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab0.Controllers;

public class CompanyController : Controller
{
    private readonly ICompanyService _companies;

    public CompanyController(ICompanyService companies)
    {
        _companies = companies;
    }

    public async Task<IActionResult> Index()
    {
        var companies = await _companies.GetAllAsync();
        return View(companies);
    }

    public IActionResult Create()
    {
        return View(new Company());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Company company)
    {
        if (!ModelState.IsValid)
        {
            return View(company);
        }
        await _companies.AddAsync(company);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        var company = await _companies.GetByIdAsync(id);
        if (company == null)
        {
            return NotFound();
        }
        return View(company);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var company = await _companies.GetByIdAsync(id);
        if (company == null)
        {
            return NotFound();
        }
        return View(company);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Company company)
    {
        if (id != company.Id)
        {
            return BadRequest();
        }
        if (!ModelState.IsValid)
        {
            return View(company);
        }
        await _companies.UpdateAsync(company);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _companies.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
