-- =====================================================
-- SISTEMA DE CONTROL DE INVENTARIO - SERVITECA CASAUTO
-- Script de creación de base de datos
-- Fecha: Marzo 2026
-- =====================================================

USE master;
GO

-- Crear base de datos
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'InventarioCasauto')
    CREATE DATABASE InventarioCasauto;
GO

USE InventarioCasauto;
GO

-- ─────────────────────────────────────────
-- TABLA: ROL
-- ─────────────────────────────────────────
CREATE TABLE ROL (
    id_rol          INT IDENTITY(1,1) PRIMARY KEY,
    nombre_rol      VARCHAR(50) NOT NULL,
    descripcion     VARCHAR(200) NULL,
    permisos        VARCHAR(MAX) NULL
);
GO

-- ─────────────────────────────────────────
-- TABLA: USUARIO
-- ─────────────────────────────────────────
CREATE TABLE USUARIO (
    id_usuario              INT IDENTITY(1,1) PRIMARY KEY,
    nombre                  VARCHAR(100) NOT NULL,
    apellido                VARCHAR(100) NOT NULL,
    email                   VARCHAR(150) NOT NULL UNIQUE,
    contrasena_hash         VARCHAR(255) NULL,
    estado                  VARCHAR(20) DEFAULT 'activo',
    fecha_creacion          DATETIME DEFAULT GETDATE(),
    fecha_ultima_modificacion DATETIME NULL,
    id_rol                  INT NOT NULL,
    FOREIGN KEY (id_rol) REFERENCES ROL(id_rol)
);
GO

-- ─────────────────────────────────────────
-- TABLA: CATEGORIA
-- ─────────────────────────────────────────
CREATE TABLE CATEGORIA (
    id_categoria        INT IDENTITY(1,1) PRIMARY KEY,
    nombre_categoria    VARCHAR(100) NOT NULL,
    descripcion         TEXT NULL,
    fecha_creacion      DATETIME DEFAULT GETDATE()
);
GO

-- ─────────────────────────────────────────
-- TABLA: PRODUCTO
-- ─────────────────────────────────────────
CREATE TABLE PRODUCTO (
    id_producto             INT IDENTITY(1,1) PRIMARY KEY,
    codigo_producto         VARCHAR(50) NOT NULL UNIQUE,
    nombre                  VARCHAR(200) NOT NULL,
    descripcion             TEXT NULL,
    id_categoria            INT NOT NULL,
    precio_unitario         DECIMAL(10,2) NOT NULL,
    precio_compra           DECIMAL(10,2) NULL,
    stock_actual            INT NOT NULL DEFAULT 0,
    stock_minimo            INT NULL DEFAULT 0,
    unidad_medida           VARCHAR(50) NULL,
    ubicacion_bodega        VARCHAR(100) NULL,
    estado                  VARCHAR(20) DEFAULT 'activo',
    fecha_creacion          DATETIME DEFAULT GETDATE(),
    fecha_ultima_modificacion DATETIME NULL,
    id_usuario_creacion     INT NULL,
    FOREIGN KEY (id_categoria) REFERENCES CATEGORIA(id_categoria),
    FOREIGN KEY (id_usuario_creacion) REFERENCES USUARIO(id_usuario)
);
GO

-- ─────────────────────────────────────────
-- TABLA: PROVEEDOR
-- ─────────────────────────────────────────
CREATE TABLE PROVEEDOR (
    id_proveedor        INT IDENTITY(1,1) PRIMARY KEY,
    nombre_proveedor    VARCHAR(200) NOT NULL,
    nit                 VARCHAR(20) NULL,
    telefono            VARCHAR(20) NULL,
    email               VARCHAR(150) NULL,
    direccion           TEXT NULL,
    estado              VARCHAR(20) DEFAULT 'activo',
    fecha_creacion      DATETIME DEFAULT GETDATE()
);
GO

-- ─────────────────────────────────────────
-- TABLA: ENTRADA_COMPRA
-- ─────────────────────────────────────────
CREATE TABLE ENTRADA_COMPRA (
    id_entrada          INT IDENTITY(1,1) PRIMARY KEY,
    numero_factura      VARCHAR(100) NULL,
    id_proveedor        INT NULL,
    fecha_compra        DATE NOT NULL,
    total_compra        DECIMAL(12,2) NULL,
    id_usuario          INT NULL,
    fecha_registro      DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (id_proveedor) REFERENCES PROVEEDOR(id_proveedor),
    FOREIGN KEY (id_usuario) REFERENCES USUARIO(id_usuario)
);
GO

-- ─────────────────────────────────────────
-- TABLA: DETALLE_ENTRADA
-- ─────────────────────────────────────────
CREATE TABLE DETALLE_ENTRADA (
    id_detalle_entrada  INT IDENTITY(1,1) PRIMARY KEY,
    id_entrada          INT NOT NULL,
    id_producto         INT NOT NULL,
    cantidad            INT NOT NULL,
    precio_unitario     DECIMAL(10,2) NOT NULL,
    subtotal            DECIMAL(12,2) NOT NULL,
    FOREIGN KEY (id_entrada) REFERENCES ENTRADA_COMPRA(id_entrada),
    FOREIGN KEY (id_producto) REFERENCES PRODUCTO(id_producto)
);
GO

-- ─────────────────────────────────────────
-- TABLA: MOVIMIENTO_INVENTARIO
-- ─────────────────────────────────────────
CREATE TABLE MOVIMIENTO_INVENTARIO (
    id_movimiento               INT IDENTITY(1,1) PRIMARY KEY,
    tipo_movimiento             VARCHAR(20) NOT NULL,
    id_producto                 INT NOT NULL,
    cantidad                    INT NOT NULL,
    precio_unitario_movimiento  DECIMAL(10,2) NULL,
    motivo                      VARCHAR(200) NULL,
    referencia_documento        VARCHAR(100) NULL,
    observaciones               TEXT NULL,
    stock_antes                 INT NOT NULL,
    stock_despues               INT NOT NULL,
    id_usuario                  INT NOT NULL,
    fecha_movimiento            DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (id_producto) REFERENCES PRODUCTO(id_producto),
    FOREIGN KEY (id_usuario) REFERENCES USUARIO(id_usuario)
);
GO

-- ─────────────────────────────────────────
-- TABLA: REGISTRO_ACTIVIDAD
-- ─────────────────────────────────────────
CREATE TABLE REGISTRO_ACTIVIDAD (
    id_registro             INT IDENTITY(1,1) PRIMARY KEY,
    tabla_afectada          VARCHAR(100) NULL,
    id_registro_afectado    INT NULL,
    accion                  VARCHAR(50) NULL,
    valores_anteriores      TEXT NULL,
    valores_nuevos          TEXT NULL,
    id_usuario              INT NULL,
    fecha_accion            DATETIME DEFAULT GETDATE(),
    ip_address              VARCHAR(50) NULL,
    descripcion_cambio      VARCHAR(500) NULL,
    FOREIGN KEY (id_usuario) REFERENCES USUARIO(id_usuario)
);
GO

-- =====================================================
-- DATOS INICIALES
-- =====================================================

-- Roles
INSERT INTO ROL (nombre_rol, descripcion) VALUES
('Administrador', 'Acceso total al sistema'),
('Operador', 'Puede registrar movimientos y gestionar productos'),
('Consulta', 'Solo puede ver información, sin modificar');
GO

-- Categorías
INSERT INTO CATEGORIA (nombre_categoria, descripcion) VALUES
('Aceites y Lubricantes', 'Aceites de motor, transmisión, dirección'),
('Filtros', 'Filtros de aire, aceite, combustible, habitáculo'),
('Frenos', 'Pastillas, discos, tambores, líquido de frenos'),
('Baterías', 'Baterías de arranque para vehículos'),
('Llantas', 'Llantas para todo tipo de vehículos'),
('Correas y Poleas', 'Distribución, serpentín, poleas'),
('Electricidad', 'Bujías, cables, fusibles, alternadores');
GO

-- Usuario administrador por defecto
INSERT INTO USUARIO (nombre, apellido, email, contrasena_hash, id_rol, estado) VALUES
('Andrea', 'Lancheros', 'andrea@casauto.com', 'hash123', 1, 'activo'),
('Nathaly', 'Camero', 'nathaly@casauto.com', 'hash456', 2, 'activo');
GO

-- Productos iniciales
INSERT INTO PRODUCTO (codigo_producto, nombre, id_categoria, precio_unitario, precio_compra, stock_actual, stock_minimo, unidad_medida, ubicacion_bodega, estado) VALUES
('ACE-001', 'Aceite Shell 20W50',        1, 48000, 32000, 24, 5, 'Litro',   'Estante A-1', 'activo'),
('ACE-002', 'Aceite Mobil 1 5W30',       1, 85000, 62000, 15, 3, 'Litro',   'Estante A-1', 'activo'),
('FIL-001', 'Filtro Aceite Mann',        2, 25000, 18000, 30, 8, 'Unidad',  'Estante B-1', 'activo'),
('FIL-002', 'Filtro Aire K&N',           2, 65000, 48000, 12, 4, 'Unidad',  'Estante B-1', 'activo'),
('FIL-003', 'Filtro Combustible Bosch',  2, 32000, 23000,  3, 5, 'Unidad',  'Estante B-2', 'activo'),
('FRE-001', 'Pastillas Brembo',          3,120000, 88000, 18, 4, 'Juego',   'Estante C-1', 'activo'),
('ELE-001', 'Batería Bosch 12V',         4,350000,265000,  8, 2, 'Unidad',  'Estante E-1', 'activo');
GO

PRINT 'Base de datos InventarioCasauto creada exitosamente.';
GO
