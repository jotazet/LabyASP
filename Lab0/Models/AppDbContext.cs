using Microsoft.EntityFrameworkCore;

namespace Lab0.Models;

public class AddDbContext : DbContext
{
    public AddDbContext(DbContextOptions<AddDbContext> options) : base(options)
    {
    }

    public DbSet<Car> Cars { get; set; }
    public DbSet<Company> Companies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>()
            .HasOne(c => c.Company)
            .WithMany(co => co.Cars)
            .HasForeignKey(c => c.CompanyId)
            .OnDelete(DeleteBehavior.SetNull);

        // Seed data dla firm
        modelBuilder.Entity<Company>()
            .HasData(
                new Company
                {
                    Id = 1,
                    Name = "AutoSalon Warszawa",
                    Address = "ul. Marszałkowska 123, 00-001 Warszawa",
                    Phone = "+48 22 123 45 67",
                    Email = "kontakt@autosalon-warszawa.pl"
                },
                new Company
                {
                    Id = 2,
                    Name = "CarDealer Kraków",
                    Address = "ul. Floriańska 45, 31-000 Kraków",
                    Phone = "+48 12 987 65 43",
                    Email = "info@cardealer-krakow.pl"
                },
                new Company
                {
                    Id = 3,
                    Name = "MotorWorld Gdańsk",
                    Address = "ul. Długa 78, 80-001 Gdańsk",
                    Phone = "+48 58 456 78 90",
                    Email = "sprzedaz@motorworld-gdansk.pl"
                }
            );

        // Seed data dla samochodów (niektóre z przypisaną firmą)
        modelBuilder.Entity<Car>()
            .HasData(
                new Car
                {
                    Id = 1,
                    Model = "Astra",
                    Manufacturer = "Opel",
                    EngineCapacity = 1.6,
                    Power = 115,
                    EngineType = "Benzyna",
                    RegistrationNumber = "PO12345",
                    Owner = "Jan Kowalski",
                    CompanyId = 1 // Przypisany do AutoSalon Warszawa
                },
                new Car
                {
                    Id = 2,
                    Model = "Civic",
                    Manufacturer = "Honda",
                    EngineCapacity = 1.8,
                    Power = 140,
                    EngineType = "Benzyna",
                    RegistrationNumber = "WA43210",
                    Owner = "Anna Nowak",
                    CompanyId = 2 // Przypisany do CarDealer Kraków
                },
                new Car
                {
                    Id = 3,
                    Model = "Corolla",
                    Manufacturer = "Toyota",
                    EngineCapacity = 1.6,
                    Power = 132,
                    EngineType = "Hybryda",
                    RegistrationNumber = "KR24680",
                    Owner = "Piotr Zieliński"
                    // Brak CompanyId - nie przypisany do żadnej firmy
                }
            );
    }
}