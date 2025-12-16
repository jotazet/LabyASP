using Lab0.Models;
using Lab0.Services;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// Konfiguracja kultury - używamy kultury niezmiennej (InvariantCulture) która używa kropki jako separatora dziesiętnego
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { CultureInfo.InvariantCulture };
    options.DefaultRequestCulture = new RequestCulture(CultureInfo.InvariantCulture);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

// Konfiguracja Entity Framework z SQLite
builder.Services.AddDbContext<AddDbContext>(options =>
    options.UseSqlite("Data Source=cars.db"));

// Używamy EF implementacji zamiast pamięci
builder.Services.AddTransient<ICarService, EfCarService>();
builder.Services.AddTransient<ICompanyService, EfCompanyService>();
var app = builder.Build();

// Automatyczne odtworzenie bazy danych przy starcie
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AddDbContext>();
    // Utwórz bazę, jeśli nie istnieje
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// Używaj konfiguracji lokalizacji
app.UseRequestLocalization();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllerRoute(
    name: "cars",
    pattern: "Cars/{action=Index}/{id?}",
    defaults: new { controller = "Car" }
);

app.Run();