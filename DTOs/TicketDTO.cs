using System.Globalization;

namespace Dashboard.Models
{
    public class TicketDTO
    {
        public int Codigo { get; set; }
        public string Titulo { get; set; }
        public string Cliente { get; set; }
        public string DataAbertura { get; set; }
        public string DataEncerramento { get; set; }
        public string Modulo { get; set; }

    }
}
