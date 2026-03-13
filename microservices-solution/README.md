

Let me explore the project to understand its architecture and conventions before drafting the constitution.   

Read [](file:///d%3A/src2/microservices-solution/README.md)

Read [](file:///d%3A/src2/microservices-solution/config/services.json)

Read [](file:///d%3A/src2/microservices-solution/microservices-solution.sln)

Read [](file:///d%3A/src2/microservices-solution/Customers/src/Program.cs)

Read [](file:///d%3A/src2/microservices-solution/Products/src/Program.cs)

Read [](file:///d%3A/src2/microservices-solution/Sales/src/Program.cs)

Read [](file:///d%3A/src2/microservices-solution/Search/src/Program.cs)

Read [](file:///d%3A/src2/microservices-solution/Customers/src/Models/Customer.cs)

Read [](file:///d%3A/src2/microservices-solution/Customers/src/Data/ICustomerRepository.cs)

Read [](file:///d%3A/src2/microservices-solution/Customers/src/Data/CustomerRepository.cs)

Read [](file:///d%3A/src2/microservices-solution/Customers/src/Presentation/CustomerEndpoints.cs)

Read [](file:///d%3A/src2/microservices-solution/Customers/Customers.csproj)

Read [](file:///d%3A/src2/microservices-solution/Products/src/Models/Product.cs)

Read [](file:///d%3A/src2/microservices-solution/Products/src/Presentation/ProductEndpoints.cs)

Read [](file:///d%3A/src2/microservices-solution/Sales/src/Models/Sale.cs)

Read [](file:///d%3A/src2/microservices-solution/Products/src/Data/ProductRepository.cs)

Read [](file:///d%3A/src2/microservices-solution/Sales/src/Data/SalesRepository.cs)

Read [](file:///d%3A/src2/microservices-solution/Sales/src/Presentation/SalesEndpoints.cs)

Searched for files matching `**/Services/**/*.cs`, 6 matches

Read [](file:///d%3A/src2/microservices-solution/Search/src/appsettings.json)

Read [](file:///d%3A/src2/microservices-solution/Sales/src/Models/CustomerSalesDto.cs)

Read [](file:///d%3A/src2/microservices-solution/Search/src/Presentation/SearchEndpoints.cs)

Read [](file:///d%3A/src2/microservices-solution/Search/src/Data/SearchRepository.cs)

Read [](file:///d%3A/src2/microservices-solution/Search/src/Services/Customers/IServiceCustomer.cs)

Read [](file:///d%3A/src2/microservices-solution/Search/src/Services/Customers/ServiceCustomer.cs)

Read [](file:///d%3A/src2/microservices-solution/Search/src/Services/Products/IServiceProduct.cs)

Read [](file:///d%3A/src2/microservices-solution/Search/src/Services/Sales/IServiceSale.cs)

Read [](file:///d%3A/src2/microservices-solution/Search/src/Services/Products/ServiceProduct.cs)

Read [](file:///d%3A/src2/microservices-solution/Search/src/Services/Sales/ServiceSale.cs)

Read [](file:///d%3A/src2/microservices-solution/Products/properties/launchSettings.json)

Read [](file:///d%3A/src2/microservices-solution/Customers/properties/launchSettings.json)

Read [](file:///d%3A/src2/microservices-solution/Sales/properties/launchSettings.json)

Read [](file:///d%3A/src2/microservices-solution/Search/properties/launchSettings.json)

Ya tengo toda la información necesaria. Aquí está la constitución del proyecto en formato Speckit:

---

# Constitución del Proyecto — microservices-solution

## 1. Identidad del Proyecto

| Campo | Valor |
|---|---|
| **Nombre** | microservices-solution |
| **Tipo** | Solución de microservicios |
| **Plataforma** | .NET 10 (ASP.NET Core Minimal APIs) |
| **Lenguaje** | C# |
| **IDE** | Visual Studio 2022+ |
| **Documentación API** | Scalar (OpenAPI) |

## 2. Visión y Propósito

Sistema distribuido compuesto por **cuatro microservicios independientes** que gestionan productos, clientes, ventas y búsqueda. Cada servicio opera de forma autónoma sin dependencias directas de compilación entre ellos. La comunicación inter-servicio se realiza exclusivamente vía **HTTP REST**.

## 3. Arquitectura

### 3.1 Topología de Servicios

```
┌──────────────┐   ┌──────────────┐   ┌──────────────┐
│  Customers   │   │   Products   │   │    Sales     │
│  :5000       │   │   :5001      │   │   :5002      │
│  /api/       │   │   /api/      │   │   /api/      │
│  customers/  │   │   products/  │   │   sales/     │
└──────┬───────┘   └──────┬───────┘   └──────┬───────┘
       │                  │                  │
       └──────────┬───────┴──────────┬───────┘
                  │                  │
           ┌──────▼──────────────────▼───────┐
           │           Search                │
           │           :5004                 │
           │   /customers, /products, /sales │
           └─────────────────────────────────┘
```

- **Customers** (`:5000`): Servicio de dominio — gestión de clientes.
- **Products** (`:5001`): Servicio de dominio — gestión de productos.
- **Sales** (`:5002`): Servicio de dominio — gestión de ventas.
- **Search** (`:5004`): Servicio agregador — consulta y compone datos de los tres servicios de dominio vía HTTP.

### 3.2 Patrón Arquitectónico por Servicio

Cada microservicio sigue una **arquitectura en capas simplificada** con tres carpetas bajo `src/`:

```
{Servicio}/
├── {Servicio}.csproj
├── properties/
│   └── launchSettings.json
└── src/
    ├── appsettings.json
    ├── Program.cs              ← Composición raíz (DI + pipeline)
    ├── Models/                 ← Entidades del dominio (POCOs)
    ├── Data/                   ← Repositorios (interfaz + implementación)
    └── Presentation/           ← Endpoints (Minimal API)
```

El servicio **Search** añade una capa adicional:

```
Search/src/
└── Services/                   ← Clientes HTTP tipados por dominio
    ├── Customers/
    │   ├── IServiceCustomer.cs
    │   └── ServiceCustomer.cs
    ├── Products/
    │   ├── IServiceProduct.cs
    │   └── ServiceProduct.cs
    └── Sales/
        ├── IServiceSale.cs
        └── ServiceSale.cs
```

## 4. Convenciones de Código

### 4.1 Nomenclatura

| Elemento | Convención | Ejemplo |
|---|---|---|
| Proyectos | PascalCase, sustantivo plural | `Customers`, `Products`, `Sales` |
| Modelos | PascalCase, sustantivo singular | `Customer`, `Product`, `Sale` |
| Repositorios | `I{Entidad}Repository` / `{Entidad}Repository` | `ICustomerRepository` / `CustomerRepository` |
| Servicios HTTP | `IService{Entidad}` / `Service{Entidad}` | `IServiceCustomer` / `ServiceCustomer` |
| Endpoints | `{Entidad}Endpoints` (clase estática) | `CustomerEndpoints`, `SalesEndpoints` |
| Método de mapeo | `Map{Entidad}Endpoints` (extensión de `IEndpointRouteBuilder`) | `MapCustomerEndpoints()` |
| Namespaces | `{Proyecto}.{Capa}` | `Customers.Presentation`, `Sales.Data`, `Sales.Models` |

### 4.2 Rutas REST

| Servicio | Base Path | Operaciones |
|---|---|---|
| Customers | `/api/customers` | GET `/`, GET `/{id}`, POST `/`, PUT `/{id}`, DELETE `/{id}` |
| Products | `/api/products` | GET `/`, GET `/{id}`, POST `/`, PUT `/{id}`, DELETE `/{id}`, POST `/filters` |
| Sales | `/api/sales` | GET `/`, GET `/{id}`, GET `/customer/{customerId}`, POST `/`, PUT `/`, DELETE `/{id}` |
| Search | `/customers`, `/products`, `/sales` | GET (proxy/agregación a servicios de dominio) |

### 4.3 Estilo de Código

- **Minimal APIs**: sin controladores. Los endpoints se definen como lambdas en clases estáticas via `MapGroup`.
- **Inyección de dependencias**: `[FromServices]` explícito en los handlers o inyección por parámetro posicional.
- **Top-level statements**: Program.cs usa el estilo moderno sin clase `Startup` ni método `Main`.
- **Primary constructors** en servicios HTTP: `public class ServiceCustomer(IHttpClientFactory _httpClientFactory)`.
- **Collection expressions**: uso de `[]` para inicializar listas inline (e.g., `return [];`).
- **Pattern matching**: uso de `is not null` / `is null` en lugar de `!= null` / `== null`.

## 5. Patrones y Principios

### 5.1 Patrones Aplicados

| Patrón | Uso |
|---|---|
| **Repository Pattern** | Cada servicio de dominio abstrae el acceso a datos tras `I{X}Repository` |
| **In-Memory Data Store** | Los repositorios usan `List<T>` como almacenamiento (sin bases de datos reales) |
| **Singleton Repository** | Los repositorios se registran como `AddSingleton` en el contenedor DI |
| **HTTP Client Factory** | `IHttpClientFactory` con clientes nombrados (`customerApi`, `productApi`, `salesApi`) |
| **API Gateway / Aggregator** | El servicio Search actúa como agregador que compone respuestas de múltiples servicios |
| **Interface Segregation** | Toda operación de datos o servicio HTTP expone una interfaz `I{X}` |

### 5.2 Principios Fundamentales

1. **Independencia de despliegue**: Cada servicio es un proyecto ASP.NET Core independiente sin referencias de proyecto entre ellos.
2. **No hay base de datos compartida**: Cada servicio posee sus propios datos (actualmente in-memory).
3. **Comunicación exclusiva vía HTTP**: Ningún servicio comparte código ni ensamblados con otro.
4. **Modelos locales por servicio**: Search define sus propios modelos (`Search.Models.Customer`, `Search.Models.Product`, `Search.Models.Sale`) en lugar de referenciar los de otros proyectos.

## 6. Infraestructura y Configuración

### 6.1 Puertos Asignados

| Servicio | Puerto HTTPS | Configurado en |
|---|---|---|
| Customers | `5000` | launchSettings.json |
| Products | `5001` | launchSettings.json |
| Sales | `5002` | launchSettings.json |
| Search | `5004` | launchSettings.json |

### 6.2 Configuración Centralizada

- config/services.json: Define las URLs base de cada servicio (referencia global).
- Cada servicio lee sus propias URLs de otros servicios desde su appsettings.json bajo la clave `Services`.

### 6.3 Dependencias Externas (NuGet)

| Paquete | Versión | Propósito |
|---|---|---|
| `Microsoft.AspNetCore.OpenApi` | 10.0.4 | Generación de especificación OpenAPI |
| `Scalar.AspNetCore` | 2.13.6 | UI interactiva para documentación API |

### 6.4 Perfiles de Lanzamiento

Cada servicio define dos perfiles:
- **`{Servicio}Web`**: Abre navegador en `/scalar/v1` con `LOG_LEVEL=Debug`.
- **`{Servicio}Cmd`**: Sin navegador, solo línea de comandos.

## 7. Flujo de Datos

### 7.1 Operaciones CRUD (Servicios de Dominio)

```
Cliente HTTP → Endpoint Minimal API → Repositorio (in-memory List<T>) → Respuesta JSON
```

### 7.2 Búsqueda Agregada (Search)

```
Cliente HTTP → SearchEndpoints
                 ├─→ IServiceCustomer → HTTP GET → Customers API → JSON
                 ├─→ IServiceProduct  → HTTP GET → Products API  → JSON
                 └─→ IServiceSale     → HTTP GET → Sales API     → JSON
                 → Composición de resultado → Respuesta JSON
```

Ejemplo concreto: `GET /sales/customer/{customerId}` en Search:
1. Obtiene datos del cliente vía `IServiceCustomer.GetCustomerById()`
2. Obtiene ventas del cliente vía `IServiceSale.GetByCustomerIdAsync()`
3. Obtiene productos de esas ventas vía `IServiceProduct.GetProductsByIdAsync()`
4. Compone y retorna el agregado `{ Customer, Sales }`.

## 8. Reglas del Proyecto

### DEBE

- Cada nuevo servicio **debe** tener su propio `.csproj` sin `<ProjectReference>` a otros servicios.
- Cada entidad **debe** tener su interfaz de repositorio y su implementación concreta.
- Los endpoints **deben** definirse en una clase estática en la carpeta `Presentation/`.
- La comunicación entre servicios **debe** hacerse vía `IHttpClientFactory` con clientes nombrados.
- Cada servicio **debe** exponer documentación OpenAPI en desarrollo.

### NO DEBE

- **No** agregar `<ProjectReference>` entre microservicios.
- **No** compartir modelos entre proyectos (cada servicio define sus propios POCOs).
- **No** usar controladores MVC — solo Minimal APIs.
- **No** usar un ORM o base de datos real en esta fase (los repositorios son in-memory).
- **No** acoplar la lógica de negocio directamente en los endpoints.

## 9. Modelo de Dominio

### Customers

```csharp
Customer { Id: int, FirstName: string, LastName: string, Email: string }
```

### Products

```csharp
Product { Id: int, Name: string, Price: decimal, Description: string }
```

### Sales

```csharp
Sale { Id: int, ProductId: int, CustomerId: int, Quantity: int }
CustomerSalesDto { Customer: Customer, Sales: IEnumerable<Sale> }
```

## 10. Deuda Técnica Conocida

| Item | Descripción | Ubicación |
|---|---|---|
| Endpoints vacíos | Los handlers CRUD de Products están definidos pero sin implementación (cuerpos vacíos) | ProductEndpoints.cs |
| Inconsistencia de namespaces | `Customers.src.Data` usa `src` en el namespace; otros servicios no | ICustomerRepository.cs |
| Modelos sin namespace | `Customer` y `Product` en los servicios de dominio no están dentro de un namespace | Customer.cs, Product.cs |
| Puerto inconsistente | `config/services.json` usa puerto `5003` para Search pero launchSettings.json usa `5004` | services.json, launchSettings.json |
| Sin validación de entrada | Los endpoints no validan los datos de entrada antes de persistir | Todos los endpoints |
| Sin manejo de errores HTTP | Los servicios HTTP en Search no manejan errores de conectividad ni reintentos | ServiceCustomer.cs, etc. |
| Async sin await | Varios métodos del repositorio son `async` pero no usan `await` internamente | ProductRepository.cs |

---

Esta constitución cubre la identidad, arquitectura, convenciones, patrones, configuración, flujos y reglas del proyecto tal como está implementado actualmente.