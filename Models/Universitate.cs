namespace WebApplication1.Models;

public class Universitate
{
    public int UniversitateID { get; set; }
    public string Nume { get; set; }
    public List<WebApplication1.Camin> Camine { get; set; } // Relație 1 la M

    public Universitate()
    {
        Camine = new List<WebApplication1.Camin>();
    }
}