using System;
using System.Collections.Generic;

namespace WebApplication1
{
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
    public class Cazare
    {
        public int CazareID { get; set; }
        public Student Student { get; set; } // Relație cu Student
        public Camera Camera { get; set; } // Relație cu Camera
    }
    public class Administrator
    {
        public int AdministratorID { get; set; }
        public string Nume { get; set; }
        public string Email { get; set; }
        public string Parola { get; set; }
    }
    public class Log
    {
        public int LogID { get; set; }
        public DateTime Data { get; set; }
        public string Mesaj { get; set; }
        public string Nivel { get; set; } // Info, Warning, Error
    }

}