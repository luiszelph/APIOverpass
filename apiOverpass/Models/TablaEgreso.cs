using System;
using System.Collections.Generic;

namespace apiOverpass.Models;

public partial class TablaEgreso
{
    public int EgresoId { get; set; }

    public string Factura { get; set; } = null!;

    public string Total { get; set; } = null!;

    public int ProveedorId { get; set; }

    public string? Comentario { get; set; }

    public DateTime FechaRegistro { get; set; }

    public DateTime FechaTicket { get; set; }

    public DateTime FechaUltimaModificación { get; set; }

    public int CreadoPorEmpleadoId { get; set; }

    public int UltimaModificacionPorEmpleadoId { get; set; }

    public int Uso { get; set; }

    public string UsoDescripcion { get; set; } = null!;
}
