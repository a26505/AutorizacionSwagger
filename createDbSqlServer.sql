CREATE DATABASE RestauranteDB;

SELECT name, database_id, create_date 
FROM sys.databases 
WHERE name = 'RestauranteDB';

USE RestauranteDB;

-- ===========================
-- TABLA USUARIO
-- ===========================
CREATE TABLE Usuario (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    Password NVARCHAR(100) NOT NULL,
    Rol NVARCHAR(20) NOT NULL
);

-- Usuario administrador inicial
INSERT INTO Usuario (Nombre, Email, Password, Rol)
VALUES ('Admin', 'admin@mail.com', '1234', 'Admin');

-- ===========================
-- TABLAS DEL RESTAURANTE
-- ===========================

CREATE TABLE PlatoPrincipal (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL CHECK (Precio >= 0),
    Ingredientes NVARCHAR(MAX) NOT NULL
);

CREATE TABLE Postre (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL CHECK (Precio >= 0),
    Calorias INT NOT NULL
);

CREATE TABLE Bebida (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL CHECK (Precio >= 0),
    EsAlcoholica BIT
);

CREATE TABLE Combo (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PlatoPrincipal INT NOT NULL,
    Bebida INT NOT NULL,
    Postre INT NOT NULL,
    Descuento DECIMAL(10, 2) NOT NULL CHECK (Descuento >= 0),

    FOREIGN KEY (PlatoPrincipal) REFERENCES PlatoPrincipal(Id),
    FOREIGN KEY (Bebida) REFERENCES Bebida(Id),
    FOREIGN KEY (Postre) REFERENCES Postre(Id)
);

-- ===========================
-- DATOS INICIALES
-- ===========================

INSERT INTO PlatoPrincipal (Nombre, Precio, Ingredientes)
VALUES 
('Plato combinado', 12.50, 'Pollo, patatas, tomate'),
('Plato vegetariano', 10.00, 'Tofu, verduras, arroz');

INSERT INTO Postre (Nombre, Precio, Calorias)
VALUES 
('Postre dulce', 5.00, 300),
('Postre dulzón', 8.00, 600);

INSERT INTO Bebida (Nombre, Precio, EsAlcoholica)
VALUES 
('Bebida mojada', 4.40, 1),
('Bebida húmeda', 5.70, 0);

INSERT INTO Combo (PlatoPrincipal, Bebida, Postre, Descuento)
VALUES 
(1, 1, 2, 0.20);

-- ===========================
-- CONSULTAS DE PRUEBA
-- ===========================

SELECT * FROM PlatoPrincipal;

SELECT * 
FROM PlatoPrincipal
WHERE Ingredientes LIKE '%Tofu%';

SELECT * 
FROM PlatoPrincipal
ORDER BY Precio ASC;

SELECT DISTINCT Nombre, Precio 
FROM PlatoPrincipal;
