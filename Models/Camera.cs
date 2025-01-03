using WebApplication1.Models;

public class Camera
{
    public int CameraID { get; set; }
    public int NumarCamera { get; set; }
    public int Capacitate { get; set; }
    public string Stare { get; set; }
    
    // Cheia externa
    public int CaminID { get; set; }
    
    // Relația cu modelul Camin
    public Camin Camin { get; set; }
}