# ProductsApi — Advanced EF Core

An ASP.NET Core Web API project demonstrating advanced Entity Framework Core concepts using SQL Server.

## Features

- **Optimized GET Endpoint:** Uses `AsNoTracking()`, projection with `Select()`, and pagination.
- **Efficient Data Retrieval:** Returns product details with category names without loading related data separately for each product.
- **Database Indexing:** Adds an index on the product name to support common search queries.
- **Soft Delete:** Uses an `IsDeleted` property and a global query filter to hide deleted products from normal queries.
- **Controlled Updates:** Uses an Update DTO to allow updates only to selected product fields.
- **Concurrency Handling:** Uses a concurrency token and handles `DbUpdateConcurrencyException`.

## Technologies

- C#
- ASP.NET Core Web API
- Entity Framework Core
- Microsoft SQL Server
- LINQ
- REST API

## Getting Started

### Prerequisites

- Visual Studio
- .NET SDK compatible with the project
- SQL Server
- SQL Server Management Studio (optional)

### Setup

1. Clone this repository.
2. Open `ProductsApi.sln` in Visual Studio, or open `ProductsApi.csproj` if no solution file exists.
3. Configure the SQL Server connection string in `appsettings.json` or User Secrets.
4. Apply the EF Core migrations to create or update the database:

   ```bash
   dotnet ef database update
   ```

5. Run the application from Visual Studio or use:

   ```bash
   dotnet run
   ```

6. Open the Swagger UI URL shown in the application output, if Swagger is enabled.

## API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/products` | Retrieve products with pagination |
| PUT | `/api/products/{id}` | Update allowed product fields |

**Pagination example:**

`GET /api/products?page=1&pageSize=10`

> Note: Additional endpoints can be documented here as they are implemented.

## Database Notes

- The project uses Entity Framework Core migrations.
- Product deletion is implemented through a soft-delete flag rather than physically removing records, provided the corresponding update logic is implemented.
- The database index and concurrency token are configured in the EF Core model.

## Project Status

A learning project focused on applying practical EF Core techniques in an ASP.NET Core Web API.

## Author

Add your name here.