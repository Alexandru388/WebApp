namespace WebApplication1.Models;

public class Camin2
{
    public int CaminID { get; set; }
    public string Nume { get; set; }
    public string Adresa { get; set; }
    public int NumarCamere { get; set; }
       
    public int UniversitateID { get; set; }  // Aici se păstrează ID-ul
    public ICollection<Camera> Camere { get; set; } // Trebuie să fie o colecție
}