using Lab0.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab0.Controllers;

public class ContactController : Controller
{
    private static Dictionary<int, Contact> _contacts = new();
    private static int _id = 0;
    // GET
    public IActionResult Index()
    {
        return View(_contacts.Values.ToList());
    }

    [HttpGet]
    public IActionResult Create() //formularz
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Contact contact) //odbiór danych
    {
        if (ModelState.IsValid)
        {
            contact.Id = _id++;
            _contacts.Add(contact.Id, contact);
            return RedirectToAction("Index");
        }
        return View(contact);
    }

    public IActionResult Details(int id)
    {
        if (_contacts.ContainsKey(id))
        {
            return View(_contacts[id]);
        }
        return View("NotFound");
    }
}