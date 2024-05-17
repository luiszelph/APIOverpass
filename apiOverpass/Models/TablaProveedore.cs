using System;
using System.Collections.Generic;

namespace apiOverpass.Models;

public partial class TablaProveedore
{
    public int ProveedorId { get; set; }

    public string ProveedorRazonSocial { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public DateTime FechaUltimaModificación { get; set; }

    public int CreadoPorEmpleadoId { get; set; }

    public int UltimaModificacionPorEmpleadoId { get; set; }

    public int Uso { get; set; }

    public string UsoDescripcion { get; set; } = null!;
}
