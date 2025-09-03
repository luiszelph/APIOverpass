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

        /// <summary>
        /// (2.1) Traer el listado de todos los Ingresos
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("ListaIngresos")]
        public async Task<ActionResult> ListaIngresos() {
            var listaIngresos = await _baseDatos.TablaIngresos.ToListAsync();
            return Ok(listaIngresos);
        }

        /// <summary>
        /// (2.2) Traer un Ingreso
        /// </summary>
        /// <param name="ingresoId"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("{ingresoId:int}", Name = "ObtenerIngreso")]
        public async Task<ActionResult<TablaIngreso>> ObtenerIngreso(int ingresoId)
        {
            try
            {
                var egreso = await _baseDatos.TablaIngresos.FirstOrDefaultAsync(x => x.IngresoId == ingresoId);

                if (egreso == null)
                {
                    return NotFound(new { mensaje = "Ingreso no encontrado" });
                }

                return Ok(egreso);
            }
            catch (Exception ex)
            {
                //return StatusCode(500, new { mensaje = "Ocurrió un error, intente nuevamente" });
                return StatusCode(500, new { mensaje = ex });
            }
        }

        /// <summary>
        /// (2.3) Metodo para insertar nuevo Egreso
        /// </summary>
        /// <param name="tablaIngreso"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("InsertarIngreso")]
        public async Task<CreatedAtRouteResult> InsertarIngreso(TablaIngreso tablaIngreso)
        {
            _baseDatos.TablaIngresos.Add(tablaIngreso);
            await _baseDatos.SaveChangesAsync();
            return CreatedAtRoute("ObtenerIngreso", new { ingresoId = tablaIngreso.IngresoId }, tablaIngreso);
        }

        /// <summary>
        /// (2.4) Actualizar un Ingreso
        /// </summary>
        /// <returns></returns>

        [HttpPut]
        [Route("{ingresoId:int}", Name = "ActualizarIngreso")]
        public async Task<ActionResult> ActualizarIngreso(int ingresoId, TablaIngreso tablaIngreso)
        {
            try
            {
                var existeIngreso = await _baseDatos.TablaIngresos.AnyAsync(x => x.IngresoId == ingresoId);

                if (!existeIngreso)
                {
                    return NotFound(new { mensaje = "Egreso no encontrado" });
                }

                _baseDatos.Update(tablaIngreso);
                await _baseDatos.SaveChangesAsync();
                return Ok("Registro actualizado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = ex });
            }

        }

        /// <summary>
        /// (2.5) Realizar baja lógica de un registro de Ingreso
        /// </summary>
        /// <param name="ingresoId"></param>
        /// <returns></returns>

        [HttpDelete]
        [Route("{ingresoId:int}", Name = "EliminarIngreso")]
        public async Task<ActionResult> EliminarIngreso(int ingresoId)
        {
            try
            {
                var filasActualizadas = await _baseDatos.TablaIngresos
                    .Where(x => x.IngresoId == ingresoId)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(e => e.Uso, 0));
                if (filasActualizadas == 0)
                {
                    return NotFound(new { mensaje = "Ingreso no encontrado" });
                }
                return Ok("Ingreso eliminado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = ex });
            }
        }
    }
}
