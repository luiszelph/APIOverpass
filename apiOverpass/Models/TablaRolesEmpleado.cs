using System;
using System.Collections.Generic;

namespace apiOverpass.Models;

public partial class TablaRolesEmpleado
{
    public int RoleId { get; set; }

    public string Rol { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateTime FechaRegistro { get; set; }

    public DateTime FechaUltimaModificación { get; set; }

    public int CreadoPorEmpleadoId { get; set; }

    public int UltimaModificacionPorEmpleadoId { get; set; }

    public int Uso { get; set; }

    public string UsoDescripcion { get; set; } = null!;
}
