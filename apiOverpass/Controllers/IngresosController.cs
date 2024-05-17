using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using apiOverpass.Models;

namespace apiOverpass.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngresosController : ControllerBase
    {
        private readonly DbOverpassContext _baseDatos;

        public IngresosController(DbOverpassContext baseDatos)
        {
            _baseDatos = baseDatos;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<ActionResult> ListaIngresos() {
            var listaIngresos = await _baseDatos.TablaIngresos.ToListAsync();
            return Ok(listaIngresos);
        }

    }
}
