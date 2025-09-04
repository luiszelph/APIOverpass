using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apiOverpass.Models;

namespace apiOverpass.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesEmpleadosController : ControllerBase
    {
        private readonly DbOverpassContext _baseDatos;

        public RolesEmpleadosController(DbOverpassContext baseDatos)
        {
            _baseDatos = baseDatos;
        }

        /// <summary>
        /// (4.1) Traer el listado de todos los RolesEmpleados
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("ListaRolesEmpleados")]
        public async Task<ActionResult> ListaRolesEmpleados()
        {
            var listaRolesEmpleados = await _baseDatos.TablaRolesEmpleados.ToListAsync();
            return Ok(listaRolesEmpleados);
        }

        /// <summary>
        /// (4.2) Traer un RolEmpleado
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("{roleId:int}", Name = "ObtenerRolEmpleado")]
        public async Task<ActionResult<TablaEmpleado>> ObtenerRolEmpleado(int roleId)
        {
            try
            {
                var rolEmpleado = await _baseDatos.TablaRolesEmpleados.FirstOrDefaultAsync(x => x.RoleId == roleId);

                if (rolEmpleado == null)
                {
                    return NotFound(new { mensaje = "Rol de empleado no encontrado" });
                }

                return Ok(rolEmpleado);
            }
            catch (Exception ex)
            {
                //return StatusCode(500, new { mensaje = "Ocurrió un error, intente nuevamente" });
                return StatusCode(500, new { mensaje = ex });
            }
        }

        /// <summary>
        /// (4.3) Metodo para insertar nuevo Rol Empleado
        /// </summary>
        /// <param name="tablaRolesEmpleado"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("InsertarRolEmpleado")]
        public async Task<CreatedAtRouteResult> InsertarRolEmpleado(TablaRolesEmpleado tablaRolesEmpleado)
        {
            _baseDatos.TablaRolesEmpleados.Add(tablaRolesEmpleado);
            await _baseDatos.SaveChangesAsync();
            return CreatedAtRoute("ObtenerEmpleado", new { empleadoId = tablaRolesEmpleado.RoleId }, tablaRolesEmpleado);
        }

        /// <summary>
        /// (4.4) Actualizar un RolEmpleado
        /// </summary>
        /// <returns></returns>

        [HttpPut]
        [Route("{roleId:int}", Name = "ActualizarRolEmpleado")]
        public async Task<ActionResult> ActualizarRolEmpleado(int roleId, TablaRolesEmpleado tablaRolesEmpleado)
        {
            try
            {
                var existeRolEmpleado = await _baseDatos.TablaRolesEmpleados.AnyAsync(x => x.RoleId == roleId);

                if (!existeRolEmpleado)
                {
                    return NotFound(new { mensaje = "Rol de empleado no encontrado" });
                }

                _baseDatos.Update(tablaRolesEmpleado);
                await _baseDatos.SaveChangesAsync();
                return Ok("Registro actualizado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = ex });
            }
        }

        /// <summary>
        /// (4.5) Realizar baja lógica de un registro de RolesEmpleados
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>

        [HttpDelete]
        [Route("{roleId:int}", Name = "EliminarRolEmpleado")]
        public async Task<ActionResult> EliminarRolEmpleado(int roleId)
        {
            try
            {
                var filasActualizadas = await _baseDatos.TablaRolesEmpleados
                    .Where(x => x.RoleId == roleId)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(e => e.Uso, 0));
                if (filasActualizadas == 0)
                {
                    return NotFound(new { mensaje = "Rol de empleado no encontrado" });
                }
                return Ok("Rol de empleado eliminado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = ex });
            }
        }
    }
}
