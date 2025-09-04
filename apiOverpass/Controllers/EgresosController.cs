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

        /// <summary>
        /// (1.1) Traer el listado de todos los Egresos
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("ListaEgresos")]
        public async Task<ActionResult> ListaEgresos()
        {
            var listaEgresos = await _baseDatos.TablaEgresos.ToListAsync();
            return Ok(listaEgresos);
        }

        /// <summary>
        /// (1.2) Traer un Egreso
        /// </summary>
        /// <param name="egresoId"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("{egresoId:int}", Name = "ObtenerEgreso")]
        public async Task<ActionResult<TablaEgreso>> ObtenerEgreso(int egresoId)
        {
            try
            {
                var egreso = await _baseDatos.TablaEgresos.FirstOrDefaultAsync(x => x.EgresoId == egresoId);

                if (egreso == null)
                {
                    return NotFound(new { mensaje = "Egreso no encontrado" });
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
        /// (1.3) Metodo para insertar nuevo Egreso
        /// </summary>
        /// <param name="tablaEgreso"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("InsertarEgreso")]
        public async Task<CreatedAtRouteResult> InsertarEgreso(TablaEgreso tablaEgreso)
        {
            _baseDatos.TablaEgresos.Add(tablaEgreso);
            await _baseDatos.SaveChangesAsync();
            return CreatedAtRoute("ObtenerEgreso", new {egresoId = tablaEgreso.EgresoId}, tablaEgreso);
        }

        /// <summary>
        /// (1.4) Actualizar un Egreso
        /// </summary>
        /// <returns></returns>

        [HttpPut]
        [Route("{egresoId:int}", Name = "ActualizarEgreso")]
        public async Task<ActionResult> ActualizarEgreso(int egresoId, TablaEgreso tablaEgreso)
        {
            try
            {
                var existeEgreso = await _baseDatos.TablaEgresos.AnyAsync(x => x.EgresoId == egresoId);

                if (!existeEgreso)
                {
                    return NotFound(new { mensaje = "Egreso no encontrado" });
                }

                _baseDatos.Update(tablaEgreso);
                await _baseDatos.SaveChangesAsync();
                return Ok("Registro actualizado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = ex });
            }
        }

        /// <summary>
        /// (1.5) Realizar baja lógica de un registro de Egreso
        /// </summary>
        /// <param name="egresoId"></param>
        /// <returns></returns>
        /// 

        [HttpDelete]
        [Route("{egresoId:int}", Name = "EliminarEgreso")]
        public async Task<ActionResult> EliminarEgreso(int egresoId)
        {
            try
            {
                var filasActualizadas = await _baseDatos.TablaEgresos
                    .Where(x => x.EgresoId == egresoId)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(e => e.Uso, 0));
               if (filasActualizadas == 0)
                {
                    return NotFound(new { mensaje = "Egreso no encontrado" });
                }
               return Ok("Egreso eliminado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = ex });
            }
        }
    }
}
