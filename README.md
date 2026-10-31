# Prueba Técnica - API de Clientes, Órdenes y Facturación

API REST en .NET 10 con C# para gestionar clientes, productos, órdenes de compra y facturas.

## Tecnologías

- .NET 10 / ASP.NET Core Web API
- Entity Framework Core
- SQLite
- Mapster

SQLite permite ejecutar el proyecto sin instalar un motor de base de datos. Mapster se usa para mapear entidades y DTOs.

## Estructura

- **Domain**: entidades e interfaces de repositorios.
- **Application**: DTOs, servicios y reglas de negocio.
- **Infrastructure**: Entity Framework Core, repositorios y migraciones.
- **Presentation**: controladores, manejo de errores y configuración.

## Ejecución

```bash
dotnet restore
dotnet run --project PruebaTecnica.Presentation
```

La API queda en `http://localhost:5103`. La base de datos se crea automáticamente con productos de ejemplo.

## Endpoints

| Método | Ruta | Descripción |
| --- | --- | --- |
| POST | `/api/customers` | Registrar un cliente |
| GET | `/api/customers` | Listar clientes |
| GET | `/api/customers/{id}` | Obtener un cliente |
| POST | `/api/products` | Registrar un producto |
| GET | `/api/products` | Listar productos |
| GET | `/api/products/{id}` | Obtener un producto |
| POST | `/api/orders` | Crear una orden con al menos dos productos |
| GET | `/api/orders` | Listar órdenes |
| GET | `/api/orders/{id}` | Obtener una orden |
| PATCH | `/api/orders/{orderId}/items/{itemId}` | Actualizar la cantidad de un producto |
| DELETE | `/api/orders/{orderId}/items/{itemId}` | Eliminar un producto y devolver el stock |
| GET | `/api/orders/{id}/invoice` | Generar el preview de la factura |

Las solicitudes de ejemplo están en `PruebaTecnica.Presentation/PruebaTecnica.http`.
