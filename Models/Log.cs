namespace WebApplication1.Models;

public class Log
{
    public int LogID { get; set; }
    public DateTime Data { get; set; }
    public string Mesaj { get; set; }
    public string Nivel { get; set; } // Info, Warning, Error
}
