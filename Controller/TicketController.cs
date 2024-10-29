using Dashboard.Models;
using Dashboard.Service;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Dashboard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController(TicketService ticketService) : Controller
    {
        private TicketService _ticketService = ticketService;

        [HttpGet("tickets/BuscarPorAnoMes")]
        public ActionResult<List<Ticket>> BuscarPorAnoMes(int mesAbertura, int anoAbertura, int mesEncerramento, int anoEncerramento)
        {

            if (((mesAbertura > mesEncerramento) && anoAbertura == anoEncerramento || (anoAbertura > anoEncerramento)))
            {
                return BadRequest("O mes ou o ano de abertura não pode ser maior que o encerramento");
            }
            var tickets = _ticketService.CarregarTicketsDoCsv();
            var ticketsFiltrados = tickets.Where(ticket =>
                (ticket.DataAbertura.Year >= anoAbertura && ticket.DataAbertura.Month >= mesAbertura) &&
                (ticket.DataEncerramento.Year <= anoEncerramento && ticket.DataEncerramento.Month <= mesEncerramento))
                .ToList();

            if (ticketsFiltrados.Count == 0)
            {
                return NotFound(new
                {
                    Mensagem = $"Não existem tickets nesse período Mes de Abertura: {mesAbertura} Ano de Abertura: {anoAbertura} a  Mes de encerramento: {mesEncerramento} Ano de Encerramento: {anoEncerramento}."
                });
            }


            return Ok(ticketsFiltrados);
        }

        [HttpGet("agrupados/modulo")]
        public ActionResult<List<Ticket>> GetTicketsAgrupadosPorModulo()
        {
            var tickets = _ticketService.CarregarTicketsDoCsv();
            var ticketsAgrupadosPorModulo = tickets
                .GroupBy(ticket => ticket.Modulo)
                .Select(grupo => new
                {
                    Modulo = grupo.Key,
                    Tickets = grupo.ToList()
                })
                .ToList();

            return Ok(ticketsAgrupadosPorModulo);
        }

        [HttpGet("agrupados/cliente")]
        public ActionResult<List<Ticket>> GetTicketsAgrupadosPorCliente()
        {
            var tickets = _ticketService.CarregarTicketsDoCsv();

            var ticketsAgrupadosPorCliente = tickets
                .GroupBy(ticket => ticket.Cliente)
                .Select(grupo => new
                {
                    Cliente = grupo.Key,
                    Tickets = grupo.ToList()
                })
                .ToList();

            return Ok(ticketsAgrupadosPorCliente);
        }

    }

    
}
