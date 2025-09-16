# API Usuarios & Domicilios (.NET 6)

REST API en .NET 6 con Entity Framework Core y arquitectura por capas (Domain, Application, Infrastructure y WebApi).
Incluye tests de integración (SQLite) y unitarios.

# 🧱 Arquitectura (Clean-ish)

Domain: entidades (Usuario, Domicilio) y contratos.

Infrastructure: AppDbContext, mapeos EF Core, repositorios (base + específicos).

Application: DTOs, mappers, servicios de negocio, validaciones.

WebApi: Controllers, middlewares, Swagger, DI.

# 🧩 Características

Clean-ish: Domain, Application, Infrastructure, WebApi.

EF Core 6 con SQLite por defecto para desarrollo y pruebas.

Soporte para MySQL/MariaDB (opcional) usando script SQL incluido.

Repositorios genéricos + repositorios específicos.

Servicios con lógica de negocio y DTOs.

Swagger/OpenAPI habilitado.

xUnit + FluentAssertions para tests.

Filtros y endpoints de búsqueda (por provincia/ciudad/nombre).

# 📁 Estructura del proyecto

/Domain                 | Entidades y contratos<br><br>
/Application            | DTOs, servicios y validaciones<br><br>
/Infrastructure         | DbContext, mapeos, repositorios<br><br>
/WebApi                 | Controllers, configuración API<br><br>
/tests<br>
&nbsp;&nbsp;&nbsp;&nbsp;/WebApi.IntegrationTests<br>
&nbsp;&nbsp;&nbsp;&nbsp;/Application.UnitTests<br>

# ✅ Requisitos

.NET SDK 6.x (o superior compatible con .NET 6)

(Opcional) MySQL/MariaDB si querés usar la DB externa

Tip: Si vas a usar VS Code, instalá las extensiones “C#” y “REST Client” (opcional). En Visual Studio 2022, basta con la carga de trabajo de ASP.NET y desarrollo web.

# 🚀 Puesta en marcha

En la raíz del repo:

dotnet restore
dotnet build


Levantá la API:

dotnet run --project WebApi


Navegá a:

Swagger: http://localhost:<puerto>/swagger

API base: http://localhost:<puerto>
(El puerto exacto lo verás en la consola al ejecutar dotnet run.)

Nota: Los tests de integración inicializan una base SQLite temporal y crean las tablas automáticamente.

# 🛢️ Usar MySQL/MariaDB

Crear la base y tablas: ejecutá el script SQL (lo tenés más abajo) en tu servidor MySQL/MariaDB.

Configurar la cadena de conexión en WebApi/appsettings.json:

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=evoltispreubatecnica;User=tu_usuario;Password=tu_password;TreatTinyAsBoolean=false;"
  }
}


# 🔌 Endpoints principales

Base: /api

Usuarios

GET /api/usuario — lista de usuarios

GET /api/usuario/{id} — usuario por id

POST /api/usuario — crea usuario

PUT /api/usuario/{id} — edita usuario (y domicilios asociados)

DELETE /api/usuario/{id} — elimina usuario

GET /api/usuario/search?nombre=&provincia=&ciudad= — búsqueda con filtros

Domicilios

GET /api/domicilio — lista de domicilios

GET /api/domicilio/{id} — domicilio por id

POST /api/domicilio — crea domicilio (requiere UsuarioId)

PUT /api/domicilio/{id} — edita domicilio

DELETE /api/domicilio/{id} — elimina domicilio

Los contratos de request/response están en la capa Application (DTOs).
Swagger muestra los modelos y permite probar todo desde el navegador.

# 🧪 Ejecutar tests

**Todos los tests:**<br><br>
&nbsp;&nbsp;&nbsp;&nbsp;dotnet test

**Solo un proyecto:**<br><br>
&nbsp;&nbsp;&nbsp;&nbsp;dotnet test tests/WebApi.IntegrationTests<br><br>
&nbsp;&nbsp;&nbsp;&nbsp;dotnet test tests/Application.UnitTests<br><br>

# 🗄️ Script SQL MySQL/MariaDB

**Se adjunta el script para crear la dB (Desplegar):**

<details> <summary>Click para desplegar</summary>
-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Versión del servidor:         10.4.28-MariaDB
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
... (omitido encabezado de settings para brevedad) ...

CREATE DATABASE IF NOT EXISTS `evoltispreubatecnica` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci */;
USE `evoltispreubatecnica`;

CREATE TABLE IF NOT EXISTS `domicilio` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UsuarioID` int(11) NOT NULL DEFAULT 0,
  `Calle` varchar(50) NOT NULL,
  `Numero` varchar(50) NOT NULL,
  `Provincia` varchar(50) NOT NULL,
  `Ciudad` varchar(50) NOT NULL,
  `FechaCreacion` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`Id`),
  KEY `FK_Usuario` (`UsuarioID`),
  CONSTRAINT `FK_Usuario` FOREIGN KEY (`UsuarioID`) REFERENCES `usuario` (`Id`) ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE IF NOT EXISTS `usuario` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(50) DEFAULT '',
  `Email` varchar(50) DEFAULT '',
  `FechaCreacion` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- (Opcional) Datos de ejemplo:
INSERT INTO `usuario` (`Id`, `Nombre`, `Email`, `FechaCreacion`) VALUES
 (1, 'Jonathan Aybar', 'jaybar@evoltis.com', '2025-09-13 10:17:22'),
 (2, 'Gabriel Humberto', 'gabhumberto@testmail.com', '1999-01-13 18:06:31'),
 (3, 'Matías Martinez', 'mmartinez@testmail.com', '2011-01-13 18:06:31'),
 (4, 'Gabriel Humberto', 'gabhumberto@testmail.com', '1999-01-13 18:06:31'),
 (5, 'Ana Perez', 'ana.perez@example.com', '2025-09-16 02:55:46');

INSERT INTO `domicilio` (`Id`, `UsuarioID`, `Calle`, `Numero`, `Provincia`, `Ciudad`, `FechaCreacion`) VALUES
 (1, 1, 'Jose Melian', '3100', 'Córdoba', 'Capital', '2025-09-13 10:19:03'),
 (2, 1, 'Fuente Alvilla', '2012', 'Córdoba', 'Capital', '2025-09-13 12:12:47'),
 (4, 1, 'Sevilla', '2012', 'Cordoba', 'Capital', '2025-09-13 12:17:47'),
 (5, 1, 'Italia', '1932', 'Roma', 'Capital', '2025-09-13 12:21:37'),
 (11, 1, 'Ibiza', '9010', 'Cordoba', 'CArloz Paz', '2025-09-13 12:44:37'),
 (12, 1, 'Ibiza', '9010', 'Cordoba', 'CArloz Paz', '2025-09-13 12:48:17'),
 (17, 3, 'Nueva Calle', '2020', 'Córdoba', 'Mendiolaza', '2025-09-16 02:26:57'),
 (24, 4, 'Av. Bulnes', '1012', 'Cordoba', 'Rio 2', '2001-09-13 12:48:17'),
 (32, 1, 'Corrientes', '1234', 'Córdoba', 'Capital', '2025-09-16 05:25:11');

</details>
🛠️ Comandos útiles (EF Core & build)
# Restaurar y compilar
dotnet restore
dotnet build

# Correr API
dotnet run --project WebApi
