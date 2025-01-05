namespace WebApplication1.Models;
using System.ComponentModel.DataAnnotations;


public class StudentViewModel
{
    [Required(ErrorMessage = "Nume is required.")]
    public string Nume { get; set; }

    [Required(ErrorMessage = "CNP is required.")]
    public string CNP { get; set; }

    [Required(ErrorMessage = "Număr Matricol is required.")]
    [RegularExpression("^[A-Za-z]{2}[0-9]{6}$", ErrorMessage = "Număr Matricol must start with 2 letters followed by 6 digits.")]
    public string NumarMatricol { get; set; }

    [Required(ErrorMessage = "Nota Cazare is required.")]
    [Range(1, 10, ErrorMessage = "Nota Cazare must be between 1 and 10.")]
    public int NotaCazare { get; set; }
}