using System;
using System.Collections.Generic;

namespace apiOverpass.Models;

public partial class TablaCliente
{
    public int ClienteId { get; set; }

    public string ClienteNombre { get; set; } = null!;

    public string ClienteSegundoNombre { get; set; } = null!;

    public string ClienteApellidoPaterno { get; set; } = null!;

    public string ClienteApellidoMaterno { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public DateTime FechaUltimaModificación { get; set; }

    public int CreadoPorEmpleadoId { get; set; }

    public int UltimaModificacionPorEmpleadoId { get; set; }

    public int Uso { get; set; }

    public string UsoDescripcion { get; set; } = null!;
}
