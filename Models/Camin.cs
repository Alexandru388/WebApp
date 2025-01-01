namespace WebApplication1.Models;

public class Camin
{
    public int CaminID { get; set; }
    public string Nume { get; set; }
    public string Adresa { get; set; }
    public int NumarCamere { get; set; }
    public Universitate Universitate { get; set; } // Relație cu Universitate
    public List<Camera> Camere { get; set; } // Relație 1 la M cu Camere

    public Camin()
    {
        Camere = new List<Camera>();
    }
}