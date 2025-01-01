namespace WebApplication1.Models;

public class Student
{
    public int StudentID { get; set; }
    public string Nume { get; set; }
    public string CNP { get; set; }
    public string NumarMatricol { get; set; }
    public decimal NotaCazare { get; set; }
    public List<Cazare> Cazari { get; set; } // Relație 1 la M cu Cazari

    public Student()
    {
        Cazari = new List<Cazare>();
    }
}