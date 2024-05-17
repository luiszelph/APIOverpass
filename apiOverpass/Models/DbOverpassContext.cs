using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace apiOverpass.Models;

public partial class DbOverpassContext : DbContext
{
    public DbOverpassContext()
    {
    }

    public DbOverpassContext(DbContextOptions<DbOverpassContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TablaCliente> TablaClientes { get; set; }

    public virtual DbSet<TablaEgreso> TablaEgresos { get; set; }

    public virtual DbSet<TablaEmpleado> TablaEmpleados { get; set; }

    public virtual DbSet<TablaIngreso> TablaIngresos { get; set; }

    public virtual DbSet<TablaProveedore> TablaProveedores { get; set; }

    public virtual DbSet<TablaRolesEmpleado> TablaRolesEmpleados { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TablaCliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("PK__TablaCli__C2FF245D71091E16");

            entity.Property(e => e.ClienteId).HasColumnName("clienteId");
            entity.Property(e => e.ClienteApellidoMaterno)
                .HasMaxLength(200)
                .HasColumnName("clienteApellidoMaterno");
            entity.Property(e => e.ClienteApellidoPaterno)
                .HasMaxLength(200)
                .HasColumnName("clienteApellidoPaterno");
            entity.Property(e => e.ClienteNombre)
                .HasMaxLength(200)
                .HasColumnName("clienteNombre");
            entity.Property(e => e.ClienteSegundoNombre)
                .HasMaxLength(200)
                .HasColumnName("clienteSegundoNombre");
            entity.Property(e => e.CreadoPorEmpleadoId).HasColumnName("creadoPorEmpleadoId");
            entity.Property(e => e.FechaRegistro)
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.FechaUltimaModificación)
                .HasColumnType("datetime")
                .HasColumnName("fechaUltimaModificación");
            entity.Property(e => e.UltimaModificacionPorEmpleadoId).HasColumnName("ultimaModificacionPorEmpleadoId");
            entity.Property(e => e.Uso)
                .HasDefaultValueSql("((1))")
                .HasColumnName("uso");
            entity.Property(e => e.UsoDescripcion)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasComputedColumnSql("(case when [uso]=(1) then 'Activo' else 'Inactivo' end)", false)
                .HasColumnName("usoDescripcion");
        });

        modelBuilder.Entity<TablaEgreso>(entity =>
        {
            entity.HasKey(e => e.EgresoId).HasName("PK__TablaEgr__22747B98EE8C2B34");

            entity.Property(e => e.EgresoId).HasColumnName("egresoId");
            entity.Property(e => e.Comentario)
                .HasMaxLength(400)
                .HasColumnName("comentario");
            entity.Property(e => e.CreadoPorEmpleadoId).HasColumnName("creadoPorEmpleadoId");
            entity.Property(e => e.Factura)
                .HasMaxLength(200)
                .HasColumnName("factura");
            entity.Property(e => e.FechaRegistro)
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.FechaTicket)
                .HasColumnType("datetime")
                .HasColumnName("fechaTicket");
            entity.Property(e => e.FechaUltimaModificación)
                .HasColumnType("datetime")
                .HasColumnName("fechaUltimaModificación");
            entity.Property(e => e.ProveedorId).HasColumnName("proveedorId");
            entity.Property(e => e.Total)
                .HasMaxLength(200)
                .HasColumnName("total");
            entity.Property(e => e.UltimaModificacionPorEmpleadoId).HasColumnName("ultimaModificacionPorEmpleadoId");
            entity.Property(e => e.Uso)
                .HasDefaultValueSql("((1))")
                .HasColumnName("uso");
            entity.Property(e => e.UsoDescripcion)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasComputedColumnSql("(case when [uso]=(1) then 'Activo' else 'Inactivo' end)", false)
                .HasColumnName("usoDescripcion");
        });

        modelBuilder.Entity<TablaEmpleado>(entity =>
        {
            entity.HasKey(e => e.EmpleadoId).HasName("PK__TablaEmp__CCDD51F819BD37E1");

            entity.Property(e => e.EmpleadoId).HasColumnName("empleadoId");
            entity.Property(e => e.CreadoPorEmpleadoId).HasColumnName("creadoPorEmpleadoId");
            entity.Property(e => e.EmpleadoApellidoMaterno)
                .HasMaxLength(200)
                .HasColumnName("empleadoApellidoMaterno");
            entity.Property(e => e.EmpleadoApellidoPaterno)
                .HasMaxLength(200)
                .HasColumnName("empleadoApellidoPaterno");
            entity.Property(e => e.EmpleadoNombre)
                .HasMaxLength(200)
                .HasColumnName("empleadoNombre");
            entity.Property(e => e.EmpleadoSegundoNombre)
                .HasMaxLength(200)
                .HasColumnName("empleadoSegundoNombre");
            entity.Property(e => e.FechaRegistro)
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.FechaUltimaModificación)
                .HasColumnType("datetime")
                .HasColumnName("fechaUltimaModificación");
            entity.Property(e => e.RolId).HasColumnName("rolId");
            entity.Property(e => e.UltimaModificacionPorEmpleadoId).HasColumnName("ultimaModificacionPorEmpleadoId");
            entity.Property(e => e.Uso)
                .HasDefaultValueSql("((1))")
                .HasColumnName("uso");
            entity.Property(e => e.UsoDescripcion)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasComputedColumnSql("(case when [uso]=(1) then 'Activo' else 'Inactivo' end)", false)
                .HasColumnName("usoDescripcion");
        });

        modelBuilder.Entity<TablaIngreso>(entity =>
        {
            entity.HasKey(e => e.IngresoId).HasName("PK__TablaIng__CC967A472BF76D9B");

            entity.Property(e => e.IngresoId).HasColumnName("ingresoId");
            entity.Property(e => e.ClienteId).HasColumnName("clienteId");
            entity.Property(e => e.Comentario)
                .HasMaxLength(400)
                .HasColumnName("comentario");
            entity.Property(e => e.CreadoPorEmpleadoId).HasColumnName("creadoPorEmpleadoId");
            entity.Property(e => e.FechaRegistro)
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.FechaTicket)
                .HasColumnType("datetime")
                .HasColumnName("fechaTicket");
            entity.Property(e => e.FechaUltimaModificación)
                .HasColumnType("datetime")
                .HasColumnName("fechaUltimaModificación");
            entity.Property(e => e.Ticket)
                .HasMaxLength(200)
                .HasColumnName("ticket");
            entity.Property(e => e.Total)
                .HasMaxLength(200)
                .HasColumnName("total");
            entity.Property(e => e.UltimaModificacionPorEmpleadoId).HasColumnName("ultimaModificacionPorEmpleadoId");
            entity.Property(e => e.Uso)
                .HasDefaultValueSql("((1))")
                .HasColumnName("uso");
            entity.Property(e => e.UsoDescripcion)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasComputedColumnSql("(case when [uso]=(1) then 'Activo' else 'Inactivo' end)", false)
                .HasColumnName("usoDescripcion");
        });

        modelBuilder.Entity<TablaProveedore>(entity =>
        {
            entity.HasKey(e => e.ProveedorId).HasName("PK__TablaPro__8253255DF083EEC3");

            entity.Property(e => e.ProveedorId).HasColumnName("proveedorId");
            entity.Property(e => e.CreadoPorEmpleadoId).HasColumnName("creadoPorEmpleadoId");
            entity.Property(e => e.FechaRegistro)
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.FechaUltimaModificación)
                .HasColumnType("datetime")
                .HasColumnName("fechaUltimaModificación");
            entity.Property(e => e.ProveedorRazonSocial)
                .HasMaxLength(400)
                .HasColumnName("proveedorRazonSocial");
            entity.Property(e => e.UltimaModificacionPorEmpleadoId).HasColumnName("ultimaModificacionPorEmpleadoId");
            entity.Property(e => e.Uso)
                .HasDefaultValueSql("((1))")
                .HasColumnName("uso");
            entity.Property(e => e.UsoDescripcion)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasComputedColumnSql("(case when [uso]=(1) then 'Activo' else 'Inactivo' end)", false)
                .HasColumnName("usoDescripcion");
        });

        modelBuilder.Entity<TablaRolesEmpleado>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__TablaRol__CD98462A1A25D0E6");

            entity.Property(e => e.RoleId).HasColumnName("roleId");
            entity.Property(e => e.CreadoPorEmpleadoId).HasColumnName("creadoPorEmpleadoId");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaRegistro)
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.FechaUltimaModificación)
                .HasColumnType("datetime")
                .HasColumnName("fechaUltimaModificación");
            entity.Property(e => e.Rol)
                .HasMaxLength(200)
                .HasColumnName("rol");
            entity.Property(e => e.UltimaModificacionPorEmpleadoId).HasColumnName("ultimaModificacionPorEmpleadoId");
            entity.Property(e => e.Uso)
                .HasDefaultValueSql("((1))")
                .HasColumnName("uso");
            entity.Property(e => e.UsoDescripcion)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasComputedColumnSql("(case when [uso]=(1) then 'Activo' else 'Inactivo' end)", false)
                .HasColumnName("usoDescripcion");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
