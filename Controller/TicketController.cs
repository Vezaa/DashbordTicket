using Dashboard.Models;
using Dashboard.Service;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController(TicketService ticketService) : Controller
    {
        private TicketService _ticketService = ticketService;

        [HttpGet("lista")]
        public ActionResult<List<Ticket>> BuscarPorAnoMes(int mesAbertura, int anoAbertura, int mesEncerramento, int anoEncerramento)
        {
            var tickets = _ticketService.CarregarTicketsDoCsv();
            var ticketsFiltrados = tickets.Where(ticket =>
                (ticket.DataAbertura.Year >= anoAbertura && ticket.DataAbertura.Month >= mesAbertura) &&
                (ticket.DataEncerramento.Year <= anoEncerramento && ticket.DataEncerramento.Month <= mesEncerramento))
                .ToList();


            return Ok(ticketsFiltrados);
        }

    }
}
