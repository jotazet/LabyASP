using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Lab0.Models;

public class Contact
{
    [HiddenInput]
    public int Id { get; set; }
    [Required(ErrorMessage ="Proszę podać imię!")]
    [StringLength(maximumLength: 50, MinimumLength = 2)]
    public string Name { get; set; }
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    [DataType(DataType.Date)]
    public DateOnly BirthDate { get; set; }
}