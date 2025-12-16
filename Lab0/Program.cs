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
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

// Konfiguracja Entity Framework z SQLite
builder.Services.AddDbContext<AddDbContext>(options =>
    options.UseSqlite("Data Source=cars.db"));

// Używamy EF implementacji zamiast pamięci
builder.Services.AddTransient<ICarService, EfCarService>();
builder.Services.AddTransient<ICompanyService, EfCompanyService>();
builder.Services.AddRazorPages();
builder.Services.AddDefaultIdentity<Microsoft.AspNetCore.Identity.IdentityUser>()
    .AddRoles<Microsoft.AspNetCore.Identity.IdentityRole>()
    .AddEntityFrameworkStores<AddDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
var app = builder.Build();

// Automatyczne odtworzenie bazy danych przy starcie
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AddDbContext>();
#if DEBUG
    // Recreate DB in development to ensure Identity tables exist
    context.Database.EnsureDeleted();
#endif
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

app.UseMiddleware<LastVisitCookie>();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapRazorPages();

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