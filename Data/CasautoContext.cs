using CasautoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CasautoAPI.Data
{
    public class CasautoContext : DbContext
    {
        public CasautoContext(DbContextOptions<CasautoContext> options)
            : base(options) { }

        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<MovimientoInventario> MovimientosInventario { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tabla ROL
            modelBuilder.Entity<Rol>().ToTable("ROL");
            modelBuilder.Entity<Rol>().HasKey(r => r.IdRol);
            modelBuilder.Entity<Rol>().Property(r => r.IdRol).HasColumnName("id_rol");
            modelBuilder.Entity<Rol>().Property(r => r.NombreRol).HasColumnName("nombre_rol");
            modelBuilder.Entity<Rol>().Property(r => r.Descripcion).HasColumnName("descripcion");
            modelBuilder.Entity<Rol>().Property(r => r.Permisos).HasColumnName("permisos");

            // Tabla USUARIO
            modelBuilder.Entity<Usuario>().ToTable("USUARIO");
            modelBuilder.Entity<Usuario>().HasKey(u => u.IdUsuario);
            modelBuilder.Entity<Usuario>().Property(u => u.IdUsuario).HasColumnName("id_usuario");
            modelBuilder.Entity<Usuario>().Property(u => u.Nombre).HasColumnName("nombre");
            modelBuilder.Entity<Usuario>().Property(u => u.Apellido).HasColumnName("apellido");
            modelBuilder.Entity<Usuario>().Property(u => u.Email).HasColumnName("email");
            modelBuilder.Entity<Usuario>().Property(u => u.ContrasenaHash).HasColumnName("contrasena_hash");
            modelBuilder.Entity<Usuario>().Property(u => u.Estado).HasColumnName("estado");
            modelBuilder.Entity<Usuario>().Property(u => u.FechaCreacion).HasColumnName("fecha_creacion");
            modelBuilder.Entity<Usuario>().Property(u => u.FechaUltimaModificacion).HasColumnName("fecha_ultima_modificacion");
            modelBuilder.Entity<Usuario>().Property(u => u.IdRol).HasColumnName("id_rol");
            modelBuilder.Entity<Usuario>().HasOne(u => u.Rol).WithMany().HasForeignKey(u => u.IdRol);

            // Tabla PRODUCTO
            modelBuilder.Entity<Producto>().ToTable("PRODUCTO");
            modelBuilder.Entity<Producto>().HasKey(p => p.IdProducto);
            modelBuilder.Entity<Producto>().Property(p => p.IdProducto).HasColumnName("id_producto");
            modelBuilder.Entity<Producto>().Property(p => p.CodigoProducto).HasColumnName("codigo_producto");
            modelBuilder.Entity<Producto>().Property(p => p.Nombre).HasColumnName("nombre");
            modelBuilder.Entity<Producto>().Property(p => p.Descripcion).HasColumnName("descripcion");
            modelBuilder.Entity<Producto>().Property(p => p.IdCategoria).HasColumnName("id_categoria");
            modelBuilder.Entity<Producto>().Property(p => p.PrecioUnitario).HasColumnName("precio_unitario");
            modelBuilder.Entity<Producto>().Property(p => p.PrecioCompra).HasColumnName("precio_compra");
            modelBuilder.Entity<Producto>().Property(p => p.StockActual).HasColumnName("stock_actual");
            modelBuilder.Entity<Producto>().Property(p => p.StockMinimo).HasColumnName("stock_minimo");
            modelBuilder.Entity<Producto>().Property(p => p.UnidadMedida).HasColumnName("unidad_medida");
            modelBuilder.Entity<Producto>().Property(p => p.UbicacionBodega).HasColumnName("ubicacion_bodega");
            modelBuilder.Entity<Producto>().Property(p => p.Estado).HasColumnName("estado");
            modelBuilder.Entity<Producto>().Property(p => p.FechaCreacion).HasColumnName("fecha_creacion");
            modelBuilder.Entity<Producto>().Property(p => p.FechaUltimaModificacion).HasColumnName("fecha_ultima_modificacion");
            modelBuilder.Entity<Producto>().Property(p => p.IdUsuarioCreacion).HasColumnName("id_usuario_creacion");

            // Tabla MOVIMIENTO_INVENTARIO
            modelBuilder.Entity<MovimientoInventario>().ToTable("MOVIMIENTO_INVENTARIO");
            modelBuilder.Entity<MovimientoInventario>().HasKey(m => m.IdMovimiento);
            modelBuilder.Entity<MovimientoInventario>().Property(m => m.IdMovimiento).HasColumnName("id_movimiento");
            modelBuilder.Entity<MovimientoInventario>().Property(m => m.TipoMovimiento).HasColumnName("tipo_movimiento");
            modelBuilder.Entity<MovimientoInventario>().Property(m => m.IdProducto).HasColumnName("id_producto");
            modelBuilder.Entity<MovimientoInventario>().Property(m => m.Cantidad).HasColumnName("cantidad");
            modelBuilder.Entity<MovimientoInventario>().Property(m => m.PrecioUnitarioMovimiento).HasColumnName("precio_unitario_movimiento");
            modelBuilder.Entity<MovimientoInventario>().Property(m => m.Motivo).HasColumnName("motivo");
            modelBuilder.Entity<MovimientoInventario>().Property(m => m.ReferenciaDocumento).HasColumnName("referencia_documento");
            modelBuilder.Entity<MovimientoInventario>().Property(m => m.Observaciones).HasColumnName("observaciones");
            modelBuilder.Entity<MovimientoInventario>().Property(m => m.StockAntes).HasColumnName("stock_antes");
            modelBuilder.Entity<MovimientoInventario>().Property(m => m.StockDespues).HasColumnName("stock_despues");
            modelBuilder.Entity<MovimientoInventario>().Property(m => m.IdUsuario).HasColumnName("id_usuario");
            modelBuilder.Entity<MovimientoInventario>().Property(m => m.FechaMovimiento).HasColumnName("fecha_movimiento");
            modelBuilder.Entity<MovimientoInventario>().HasOne(m => m.Producto).WithMany().HasForeignKey(m => m.IdProducto);
            modelBuilder.Entity<MovimientoInventario>().HasOne(m => m.Usuario).WithMany().HasForeignKey(m => m.IdUsuario);

            // Tabla CATEGORIA
            modelBuilder.Entity<Categoria>().ToTable("CATEGORIA");
            modelBuilder.Entity<Categoria>().HasKey(c => c.IdCategoria);
            modelBuilder.Entity<Categoria>().Property(c => c.IdCategoria).HasColumnName("id_categoria");
            modelBuilder.Entity<Categoria>().Property(c => c.NombreCategoria).HasColumnName("nombre_categoria");
            modelBuilder.Entity<Categoria>().Property(c => c.Descripcion).HasColumnName("descripcion");
            modelBuilder.Entity<Categoria>().Property(c => c.FechaCreacion).HasColumnName("fecha_creacion");
        }
    }
}