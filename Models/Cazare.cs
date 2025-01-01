namespace WebApplication1.Models;

public class Cazare
{
    public int CazareID { get; set; }
    public Student Student { get; set; } // Relație cu Student
    public Camera Camera { get; set; } // Relație cu Camera
}