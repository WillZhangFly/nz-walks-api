# New Zealand Walks API

A RESTful API for managing walking trails in New Zealand, built with ASP.NET Core and Entity Framework.

## Features

- **Regions Management**: CRUD operations for New Zealand regions
- **Walks Management**: CRUD operations for walking trails
- **Difficulty Levels**: Easy, Medium, and Hard classifications
- **Data Filtering**: Filter walks by name and other criteria
- **Database Integration**: SQL Server with Entity Framework Core
- **API Documentation**: Swagger/OpenAPI support

## Technologies Used

- **ASP.NET Core 9.0**
- **Entity Framework Core**
- **SQL Server** (via Docker with Azure SQL Edge)
- **AutoMapper** for object mapping
- **Swagger/OpenAPI** for API documentation

## Getting Started

### Prerequisites

- .NET 9.0 SDK
- Docker (for SQL Server)
- Visual Studio Code or Visual Studio

### Installation

1. Clone the repository:
```bash
git clone https://github.com/WillZhangFly/nz-walks-api.git
cd nz-walks-api
```

2. Start SQL Server using Docker:
```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Password123" -p 1433:1433 --name sql-server-container -d mcr.microsoft.com/azure-sql-edge
```

3. Update the database:
```bash
dotnet ef database update
```

4. Seed the database with sample data:
```bash
curl -X POST "https://localhost:5001/api/DataSeed/execute-full-sql-script"
```

5. Run the application:
```bash
dotnet run
```

The API will be available at `https://localhost:5001` or `http://localhost:5000`.

## API Endpoints

### Regions
- `GET /api/regions` - Get all regions
- `GET /api/regions/{id}` - Get region by ID
- `POST /api/regions` - Create new region
- `PUT /api/regions/{id}` - Update region
- `DELETE /api/regions/{id}` - Delete region

### Walks
- `GET /api/walks` - Get all walks (supports filtering)
- `GET /api/walks/{id}` - Get walk by ID
- `POST /api/walks` - Create new walk
- `PUT /api/walks/{id}` - Update walk
- `DELETE /api/walks/{id}` - Delete walk

### Data Seeding
- `POST /api/DataSeed/execute-full-sql-script` - Seed database with sample data

## Sample Data

The API includes sample data for:
- **6 Regions**: Auckland, Wellington, Nelson, Bay of Plenty, Southland, Northland
- **16 Walks**: Various trails across New Zealand with different difficulty levels
- **3 Difficulty Levels**: Easy, Medium, Hard

## Project Structure

```
NZWalks/
├── Controllers/         # API Controllers
├── Data/               # Entity Framework DbContext
├── Models/             # Domain models and DTOs
├── Repositories/       # Data access layer
├── Migrations/         # EF Core migrations
├── Mapping/            # AutoMapper profiles
└── Properties/         # Launch settings
```

## Configuration

Update `appsettings.json` with your database connection string:

```json
{
  "ConnectionStrings": {
    "NZWalksConnectionString": "Server=localhost,1433;Database=NZWalksDb;User Id=sa;Password=Password123;TrustServerCertificate=True;Encrypt=False;"
  }
}
```

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License

This project is licensed under the MIT License.

## Author

Will Zhang - [GitHub Profile](https://github.com/WillZhangFly)
