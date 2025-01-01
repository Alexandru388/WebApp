namespace WebApplication1.Models;

public class Universitate
{
    public int UniversitateID { get; set; }
    public string Nume { get; set; }
    public List<Camin> Camine { get; set; } // Relație 1 la M

    public Universitate()
    {
        Camine = new List<Camin>();
    }
}