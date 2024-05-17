using System;
using System.Collections.Generic;

namespace apiOverpass.Models;

public partial class TablaEmpleado
{
    public int EmpleadoId { get; set; }

    public string EmpleadoNombre { get; set; } = null!;

    public string EmpleadoSegundoNombre { get; set; } = null!;

    public string EmpleadoApellidoPaterno { get; set; } = null!;

    public string EmpleadoApellidoMaterno { get; set; } = null!;

    public int RolId { get; set; }

    public DateTime FechaRegistro { get; set; }

    public DateTime FechaUltimaModificación { get; set; }

    public int CreadoPorEmpleadoId { get; set; }

    public int UltimaModificacionPorEmpleadoId { get; set; }

    public int Uso { get; set; }

    public string UsoDescripcion { get; set; } = null!;
}
