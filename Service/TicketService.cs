using CsvHelper.Configuration;
using CsvHelper;
using Dashboard.Models;
using System.Globalization;

namespace Dashboard.Service
{
    public class TicketService
    {
        private readonly List<TicketDTO> _tickets;

        public List<Ticket> CarregarTicketsDoCsv()
        {
            var ticketsDTO = new List<TicketDTO>();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ","
            };

            using (var reader = new StreamReader("./TicketsRecuperados.csv"))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Context.RegisterClassMap<TicketMap>();
                ticketsDTO = csv.GetRecords<TicketDTO>().ToList();
            }

            return ticketsDTO.Select(dto => new Ticket
            {
                Codigo = dto.Codigo,
                Titulo = dto.Titulo,
                Cliente = dto.Cliente,
                DataAbertura = DateOnly.ParseExact(dto.DataAbertura, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                DataEncerramento = DateOnly.ParseExact(dto.DataEncerramento, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                Modulo = dto.Modulo
            }).ToList();
        }
        public TicketService()
        {
            _tickets = new List<TicketDTO>();
        }
    }

    public class TicketMap : ClassMap<TicketDTO>
    {
        public TicketMap()
        {
            Map(m => m.Codigo).Name("Codigo");
            Map(m => m.Titulo).Name("Titulo");
            Map(m => m.Cliente).Name("Cliente");
            Map(m => m.DataAbertura).Name("DataAbertura");
            Map(m => m.DataEncerramento).Name("DataEncerramento");
            Map(m => m.Modulo).Name("Modulo");
        }
    }
}
