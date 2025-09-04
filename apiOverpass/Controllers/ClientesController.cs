using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apiOverpass.Models;

namespace apiOverpass.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly DbOverpassContext _baseDatos;

        public ClientesController(DbOverpassContext baseDatos)
        {
            _baseDatos = baseDatos;
        }

        /// <summary>
        /// (5.1) Traer el listado de todos los Clientes
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("ListaClientes")]
        public async Task<ActionResult> ListaClientes()
        {
            var listaClientes = await _baseDatos.TablaClientes.ToListAsync();
            return Ok(listaClientes);
        }

        /// <summary>
        /// (5.2) Traer un Cliente
        /// </summary>
        /// <param name="clienteId"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("{clienteId:int}", Name = "ObtenerCliente")]
        public async Task<ActionResult<TablaCliente>> ObtenerCliente(int clienteId)
        {
            try
            {
                var cliente = await _baseDatos.TablaClientes.FirstOrDefaultAsync(x => x.ClienteId == clienteId);

                if (cliente == null)
                {
                    return NotFound(new { mensaje = "Cliente no encontrado" });
                }

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                //return StatusCode(500, new { mensaje = "Ocurrió un error, intente nuevamente" });
                return StatusCode(500, new { mensaje = ex });
            }
        }

        /// <summary>
        /// (5.3) Metodo para insertar nuevo Cliente
        /// </summary>
        /// <param name="tablaCliente"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("InsertarCliente")]
        public async Task<CreatedAtRouteResult> InsertarCliente(TablaCliente tablaCliente)
        {
            _baseDatos.TablaClientes.Add(tablaCliente);
            await _baseDatos.SaveChangesAsync();
            return CreatedAtRoute("ObtenerCliente", new { clienteId = tablaCliente.ClienteId }, tablaCliente);
        }

        /// <summary>
        /// (5.4) Actualizar un Cliente
        /// </summary>
        /// <returns></returns>
        //5.4
        [HttpPut]
        [Route("{clienteId:int}", Name = "ActualizarCliente")]
        public async Task<ActionResult> ActualizarCliente(int clienteId, TablaCliente tablaCliente)
        {
            try
            {
                var existeCliente = await _baseDatos.TablaClientes.AnyAsync(x => x.ClienteId == clienteId);

                if (!existeCliente)
                {
                    return NotFound(new { mensaje = "Cliente no encontrado" });
                }

                _baseDatos.Update(tablaCliente);
                await _baseDatos.SaveChangesAsync();
                return Ok("Registro actualizado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = ex });
            }
        }

        /// <summary>
        /// (5.5) Realizar baja lógica de un registro de Clientes
        /// </summary>
        /// <param name="clienteId"></param>
        /// <returns></returns>

        [HttpDelete]
        [Route("{clienteId:int}", Name = "EliminarCliente")]
        public async Task<ActionResult> EliminarCliente(int clienteId)
        {
            try
            {
                var filasActualizadas = await _baseDatos.TablaClientes
                    .Where(x => x.ClienteId == clienteId)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(e => e.Uso, 0));
                if (filasActualizadas == 0)
                {
                    return NotFound(new { mensaje = "Cliente no encontrado" });
                }
                return Ok("Cliente eliminado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = ex });
            }
        }
    }
}
