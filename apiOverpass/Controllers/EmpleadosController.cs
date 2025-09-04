using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apiOverpass.Models;

namespace apiOverpass.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly DbOverpassContext _baseDatos;

        public EmpleadosController(DbOverpassContext baseDatos)
        {
            _baseDatos = baseDatos;
        }

        /// <summary>
        /// (3.1) Traer el listado de todos los Empleados
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("ListaEmpleados")]
        public async Task<ActionResult> ListaEmpleados()
        {
            var listaEmpleados = await _baseDatos.TablaEmpleados.ToListAsync();
            return Ok(listaEmpleados);
        }

        /// <summary>
        /// (3.2) Traer un Empleado
        /// </summary>
        /// <param name="ingresoId"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("{empleadoId:int}", Name = "ObtenerEmpleado")]
        public async Task<ActionResult<TablaEmpleado>> ObtenerEmpleado(int empleadoId)
        {
            try
            {
                var empleado = await _baseDatos.TablaEmpleados.FirstOrDefaultAsync(x => x.EmpleadoId == empleadoId);

                if (empleado == null)
                {
                    return NotFound(new { mensaje = "Empleado no encontrado" });
                }

                return Ok(empleado);
            }
            catch (Exception ex)
            {
                //return StatusCode(500, new { mensaje = "Ocurrió un error, intente nuevamente" });
                return StatusCode(500, new { mensaje = ex });
            }
        }

        /// <summary>
        /// (3.3) Metodo para insertar nuevo Empleado
        /// </summary>
        /// <param name="tablaEmpleado"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("InsertarEmpleado")]
        public async Task<CreatedAtRouteResult> InsertarEmpleado(TablaEmpleado tablaEmpleado)
        {
            _baseDatos.TablaEmpleados.Add(tablaEmpleado);
            await _baseDatos.SaveChangesAsync();
            return CreatedAtRoute("ObtenerEmpleado", new { empleadoId = tablaEmpleado.EmpleadoId }, tablaEmpleado);
        }

        /// <summary>
        /// (3.4) Actualizar un Empleado
        /// </summary>
        /// <returns></returns>

        [HttpPut]
        [Route("{empleadoId:int}", Name = "ActualizarEmpleado")]
        public async Task<ActionResult> ActualizarEmpleado(int empleadoId, TablaEmpleado tablaEmpleado)
        {
            try
            {
                var existeEmpleado = await _baseDatos.TablaEmpleados.AnyAsync(x => x.EmpleadoId == empleadoId);

                if (!existeEmpleado)
                {
                    return NotFound(new { mensaje = "Empleado no encontrado" });
                }

                _baseDatos.Update(tablaEmpleado);
                await _baseDatos.SaveChangesAsync();
                return Ok("Registro actualizado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = ex });
            }
        }

        /// <summary>
        /// (3.5) Realizar baja lógica de un registro de Empleados
        /// </summary>
        /// <param name="empleadoId"></param>
        /// <returns></returns>

        [HttpDelete]
        [Route("{empleadoId:int}", Name = "EliminarEmpleado")]
        public async Task<ActionResult> EliminarEmpleado(int empleadoId)
        {
            try
            {
                var filasActualizadas = await _baseDatos.TablaEmpleados
                    .Where(x => x.EmpleadoId == empleadoId)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(e => e.Uso, 0));
                if (filasActualizadas == 0)
                {
                    return NotFound(new { mensaje = "Empleado no encontrado" });
                }
                return Ok("Empleado eliminado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = ex });
            }
        }
    }
}
