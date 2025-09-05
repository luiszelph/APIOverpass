using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apiOverpass.Models;

namespace apiOverpass.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {
        private readonly DbOverpassContext _baseDatos;

        public ProveedoresController(DbOverpassContext baseDatos)
        {
            _baseDatos = baseDatos;
        }

        /// <summary>
        /// (6.1) Traer el listado de todos los Proveedores
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("ListaProveedores")]
        public async Task<ActionResult> ListaProveedores()
        {
            var listaProveedores = await _baseDatos.TablaProveedores.ToListAsync();
            return Ok(listaProveedores);
        }

        /// <summary>
        /// (6.2) Traer un Proveedor
        /// </summary>
        /// <param name="proveedorId"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("{proveedorId:int}", Name = "ObtenerProveedor")]
        public async Task<ActionResult<TablaProveedore>> ObtenerProveedor(int proveedorId)
        {
            try
            {
                var proveedor = await _baseDatos.TablaProveedores.FirstOrDefaultAsync(x => x.ProveedorId == proveedorId);

                if (proveedor == null)
                {
                    return NotFound(new { mensaje = "Proveedor no encontrado" });
                }

                return Ok(proveedor);
            }
            catch (Exception ex)
            {
                //return StatusCode(500, new { mensaje = "Ocurrió un error, intente nuevamente" });
                return StatusCode(500, new { mensaje = ex });
            }
        }

        /// <summary>
        /// (6.3) Metodo para insertar nuevo Proveedor
        /// </summary>
        /// <param name="tablaProveedor"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("InsertarProveedor")]
        public async Task<CreatedAtRouteResult> InsertarProveedor(TablaProveedore tablaProveedor)
        {
            _baseDatos.TablaProveedores.Add(tablaProveedor);
            await _baseDatos.SaveChangesAsync();
            return CreatedAtRoute("ObtenerProveedor", new { proveedorId = tablaProveedor.ProveedorId }, tablaProveedor);
        }

        /// <summary>
        /// (6.4) Actualizar un Proveedor
        /// </summary>
        /// <returns></returns>

        [HttpPut]
        [Route("{proveedorId:int}", Name = "ActualizarProveedor")]
        public async Task<ActionResult> ActualizarProveedor(int proveedorId, TablaProveedore tablaProveedor)
        {
            try
            {
                var existeProveedor = await _baseDatos.TablaProveedores.AnyAsync(x => x.ProveedorId == proveedorId);

                if (!existeProveedor)
                {
                    return NotFound(new { mensaje = "Proveedor no encontrado" });
                }

                _baseDatos.Update(tablaProveedor);
                await _baseDatos.SaveChangesAsync();
                return Ok("Registro actualizado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = ex });
            }
        }

        /// <summary>
        /// (6.5) Realizar baja lógica de un registro de Proveedor
        /// </summary>
        /// <param name="clienteId"></param>
        /// <returns></returns>

        [HttpDelete]
        [Route("{proveedorId:int}", Name = "EliminarProveedor")]
        public async Task<ActionResult> EliminarProveedor(int proveedorId)
        {
            try
            {
                var filasActualizadas = await _baseDatos.TablaProveedores
                    .Where(x => x.ProveedorId == proveedorId)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(e => e.Uso, 0));
                if (filasActualizadas == 0)
                {
                    return NotFound(new { mensaje = "Proveedor no encontrado" });
                }
                return Ok("Proveedor eliminado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = ex });
            }
        }
    }
}
