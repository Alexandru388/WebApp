namespace WebApplication1.Models;

public class Camera
{
    public int CameraID { get; set; }
    public int NumarCamera { get; set; }
    public int Capacitate { get; set; }
    public string Stare { get; set; } // Disponibila sau Ocupata
    public Camin Camin { get; set; } // Relație cu Camin
    public List<Cazare> Cazari { get; set; } // Relație 1 la M cu Cazari

    public Camera()
    {
        Cazari = new List<Cazare>();
    }
}