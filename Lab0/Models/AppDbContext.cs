using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Lab0.Models;

public class AddDbContext : IdentityDbContext<IdentityUser>
{
    public AddDbContext(DbContextOptions<AddDbContext> options) : base(options)
    {
    }

    public DbSet<Car> Cars { get; set; }
    public DbSet<Company> Companies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=cars.db");
        }
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Car>()
            .HasOne(c => c.Company)
            .WithMany(co => co.Cars)
            .HasForeignKey(c => c.CompanyId)
            .OnDelete(DeleteBehavior.SetNull);

        // Identity seed: roles and users
        const string ADMIN_ROLE_ID = "5b8a7f83-7d87-4ab5-9d8a-1f1e5f4a1a01";
        const string USER_ROLE_ID = "8c9b9c21-3fd1-4e8e-9b5a-0b1a2c3d4e5f";
        const string ADMIN_ID = "e3d8f7b0-9c1a-4e88-86f0-221b9b4c7c01";
        const string USER_ID = "a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d";

        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = ADMIN_ROLE_ID, Name = "admin", NormalizedName = "ADMIN", ConcurrencyStamp = ADMIN_ROLE_ID },
            new IdentityRole { Id = USER_ROLE_ID, Name = "user", NormalizedName = "USER", ConcurrencyStamp = USER_ROLE_ID }
        );

        var admin = new IdentityUser
        {
            Id = ADMIN_ID,
            Email = "adam@wsei.edu.pl",
            EmailConfirmed = true,
            UserName = "adam",
            NormalizedUserName = "ADAM",
            NormalizedEmail = "ADAM@WSEI.EDU.PL"
        };
        var user = new IdentityUser
        {
            Id = USER_ID,
            Email = "user@wsei.edu.pl",
            EmailConfirmed = true,
            UserName = "user",
            NormalizedUserName = "USER",
            NormalizedEmail = "USER@WSEI.EDU.PL"
        };
        var ph = new PasswordHasher<IdentityUser>();
        admin.PasswordHash = ph.HashPassword(admin, "1234abcd!@#$ABCD");
        user.PasswordHash = ph.HashPassword(user, "User1234!@#$abcd");

        modelBuilder.Entity<IdentityUser>().HasData(admin, user);
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { RoleId = ADMIN_ROLE_ID, UserId = ADMIN_ID },
            new IdentityUserRole<string> { RoleId = USER_ROLE_ID, UserId = USER_ID }
        );

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
                    CompanyId = 1
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
                    CompanyId = 2
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
                }
            );
    }
}