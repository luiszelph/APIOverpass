using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using apiOverpass.Models;

namespace apiOverpass.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EgresosController : ControllerBase
    {
        private readonly DbOverpassContext _baseDatos;

        public EgresosController(DbOverpassContext baseDatos)
        {
            _baseDatos = baseDatos;
        }

        [HttpGet]
        [Route("ListaEgresos")]
        public async Task<ActionResult> ListaEgresos()
        {
            var listaEgresos = await _baseDatos.TablaEgresos.ToListAsync();
            return Ok(listaEgresos);
        }

    }
}
