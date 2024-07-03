using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apiOverpass.Models;
using System.Collections.Generic;

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
        public async Task<ActionResult> ListaIngresos()
        {
            try
            {
                var listaIngresos = await _baseDatos.TablaIngresos.ToListAsync();
                return Ok(listaIngresos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los datos de la base de datos");
            }
        }

    }
}
