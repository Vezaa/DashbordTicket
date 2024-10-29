using System.Globalization;

namespace Dashboard.Models
{
    public class Ticket
    {
       public int Codigo { get; set; }
       public string Titulo { get; set; }
       public string Cliente { get; set; }
       public DateOnly DataAbertura { get; set; }
       public DateOnly DataEncerramento { get; set; }
       public string Modulo { get; set; }
        
    }
}
