using System.ComponentModel.DataAnnotations;

namespace Lab0.Models;

public class Car
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Model jest wymagany")]
    [StringLength(100, ErrorMessage = "Model nie może przekraczać 100 znaków")]
    public string Model { get; set; } = string.Empty;

    [Required(ErrorMessage = "Producent jest wymagany")]
    [StringLength(100, ErrorMessage = "Producent nie może przekraczać 100 znaków")]
    public string Manufacturer { get; set; } = string.Empty;

    [Required(ErrorMessage = "Pojemność silnika jest wymagana")]
    [Range(0.1, 20.0, ErrorMessage = "Pojemność silnika musi być między 0.1 a 20.0 litrów")]
    [Display(Name = "Pojemność silnika (l)")]
    public double EngineCapacity { get; set; }

    [Required(ErrorMessage = "Moc jest wymagana")]
    [Range(1, 2000, ErrorMessage = "Moc musi być między 1 a 2000 KM")]
    [Display(Name = "Moc (KM)")]
    public int Power { get; set; }

    [Required(ErrorMessage = "Rodzaj silnika jest wymagany")]
    [StringLength(50, ErrorMessage = "Rodzaj silnika nie może przekraczać 50 znaków")]
    [Display(Name = "Rodzaj silnika")]
    public string EngineType { get; set; } = string.Empty;

    [Required(ErrorMessage = "Numer rejestracyjny jest wymagany")]
    [RegularExpression(@"^[A-Z]{2,3}\s?[A-Z0-9]{4,5}$", ErrorMessage = "Nieprawidłowy format numeru rejestracyjnego")]
    [Display(Name = "Nr rejestracyjny")]
    public string RegistrationNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Właściciel jest wymagany")]
    [StringLength(200, ErrorMessage = "Właściciel nie może przekraczać 200 znaków")]
    [Display(Name = "Właściciel")]
    public string Owner { get; set; } = string.Empty;

    // Opcjonalna relacja z firmą
    [Display(Name = "Firma")]
    public int? CompanyId { get; set; }
    
    public virtual Company? Company { get; set; }
}