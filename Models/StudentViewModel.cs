namespace WebApplication1.Models;
using System.ComponentModel.DataAnnotations;


public class StudentViewModel
{
    [Required(ErrorMessage = "Nume is required.")]
    //[RegularExpression("^[A-Z][a-z]*$", ErrorMessage = "Numele trebuie sa inceapa cu litera mare si sa continue cu litere mici")]
    public string Nume { get; set; }

    [Required(ErrorMessage = "CNP is required.")]
    [RegularExpression("[0-9]{13}", ErrorMessage = "CNP ul trebuie sa aibe exact 13 cifre")]

    public string CNP { get; set; }

    [Required(ErrorMessage = "Număr Matricol is required.")]
    [RegularExpression("^[A-Za-z]{2}[0-9]{6}$", ErrorMessage = "Număr Matricol must start with 2 letters followed by 6 digits.")]
    public string NumarMatricol { get; set; }

    [Required(ErrorMessage = "Nota Cazare is required.")]
    [Range(1, 10, ErrorMessage = "Nota Cazare trebuie sa fie intre 1 and 10.")]
    public decimal NotaCazare { get; set; }
}