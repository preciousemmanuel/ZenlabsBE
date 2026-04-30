# ZentekLabs Products API

A production-grade .NET 6 Web API for managing products with JWT authentication, PostgreSQL integration, pagination, filtering, logging, unit testing, and clean architecture principles.

---

# Features Implemented

- .NET 6 Web API
- JWT Authentication & Authorization
- Public health check endpoint
- Secured product endpoints
- PostgreSQL database integration
- Entity Framework Core with migrations
- Repository Pattern with Dependency Injection
- AutoMapper for DTO mapping
- Serilog structured logging
- Pagination support
- Product filtering
- Global exception handling
- Unit testing using xUnit, FluentAssertions, and FakeItEasy
- Swagger/OpenAPI documentation

---

# Technologies Used

| Technology | Purpose |
|---|---|
| ASP.NET Core (.NET 6) | Web API Framework |
| PostgreSQL | Database |
| Entity Framework Core | ORM |
| JWT Bearer Authentication | API Security |
| AutoMapper | Object Mapping |
| Serilog | Structured Logging |
| xUnit | Unit Testing |
| FluentAssertions | Assertions |
| FakeItEasy | Mocking/Fakes |
| Swagger | API Documentation |

---

# Authentication

JWT Bearer Authentication was implemented to secure the API endpoints.

The following endpoints require authentication:

- Create Product
- Get Products
- Get Product By Id

---

# Login Credentials

Use the following credentials to generate a JWT token:

```txt
Username: admin
Password: password
```

---

# Authentication Flow

1. Login using the authentication endpoint
2. Receive JWT token
3. Add token to Authorization header

Example:

```http
Authorization: Bearer your-jwt-token
```

---

# API Endpoints

## Public Endpoint

### Health Check

```http
GET /api/health
```

Response:

```json
{
  "status": "OK"
}
```

---

# Authentication Endpoint

### Login

```http
POST /api/auth/login
```

Request:

```json
{
  "username": "admin",
  "password": "password"
}
```

---

# Product Endpoints

## Create Product

```http
POST /api/products
```

---

## Get All Products

```http
GET /api/products
```

Supports:

- Pagination
- Search
- Date filtering
- Active status filtering

Example:

```http
GET /api/products?page=1&pageSize=10&search=iphone
```

---

## Get Product By Id

```http
GET /api/products/{id}
```

---

# Project Architecture

The project follows clean architecture principles using:

- Dependency Injection
- Repository Pattern
- Service Layer Separation
- DTO Mapping
- Structured Logging

---

# Project Structure

```txt
ZentekLabs/
│
├── Controllers/
├── Data/
├── Middleware/
├── Models/
│   ├── Domain/
│   ├── Dtos/
│
├── Repositories/
│   ├── Interfaces/
│   ├── Implementations/
│

├── Mapping/
├── Migrations/
├── Tests/
│
└── Program.cs
```

---

# Database

PostgreSQL was used as the primary database.

Entity Framework Core migrations were implemented for database versioning and schema management.

---

# Running Migrations

```bash
dotnet ef database update
```

---

# Logging

Serilog was implemented for structured application logging.

Logs are written to:

- Console
- File system

This helps with:

- Monitoring
- Debugging
- Error tracking
- Production observability

---

# Pagination

Paginated responses were implemented for product listing endpoints.

Example response structure:

```json
{
  "sucesss": true,
  "message": "Products retrieved successfully.",
  "data": {
    "items": [],
    "totalCount": 20,
    "page": 1,
    "pageSize": 10,
    "totalPages": 2
  }
}
```

---

# Unit Testing

Unit tests were implemented using:

- xUnit
- FluentAssertions
- FakeItEasy

The following layers were tested:

- Repository Layer
- Service Layer
- Controller Layer

Testing includes:

- Product creation
- Pagination
- Filtering
- Retrieval by Id
- Error scenarios

---

# Swagger

Swagger/OpenAPI documentation was configured for API testing and endpoint exploration.

Swagger includes JWT authentication support for secured endpoint testing.

---

# Running the Application

## Clone the repository

```bash
git clone <repository-url>
```

---

## Restore packages

```bash
dotnet restore
```

---

## Update database

```bash
dotnet ef database update
```

---

## Run the application

```bash
dotnet run
```

---

# Distributed / Microservices Event-Driven Architecture

The Products API can form part of a larger distributed microservices architecture.

Example architecture:

```txt
                    +-------------------+
                    |   Client Apps     |
                    | Web / Mobile Apps |
                    +---------+---------+
                              |
                              v
                    +-------------------+
                    |    API Gateway    |
                    | Authentication    |
                    | Rate Limiting     |
                    +---------+---------+
                              |
        ------------------------------------------------
        |                    |                         |
        v                    v                         v

+---------------+   +----------------+      +----------------+
| Product       |   | Order Service  |      | Payment Service|
| Service       |   |                |      |                |
+-------+-------+   +--------+-------+      +--------+-------+
        |                       |                     |
        v                       v                     v
+---------------+     +----------------+     +----------------+
| Product DB    |     | Orders DB      |     | Payments DB    |
+---------------+     +----------------+     +----------------+

        \                 |                     /
         \                |                    /
          \               |                   /
           -----------------------------------
                           |
                           v
                 +-------------------+
                 | Message Broker    |
                 | RabbitMQ / Kafka  |
                 +-------------------+
```

---

# Event-Driven Communication

In a real-world architecture:

- Product Service can publish `ProductCreated` events
- Order Service can publish `OrderCreated` events
- Payment Service can publish `PaymentCompleted` events

Services communicate asynchronously through a message broker such as:

- RabbitMQ
- Kafka

This provides:

- Loose coupling
- Scalability
- Fault isolation
- Independent deployments

---

# Improvements That Can Be Added

Potential future improvements:

- Docker support
- Redis caching
- API versioning
- Kubernetes deployment
- CI/CD pipelines
- OpenTelemetry distributed tracing
- Role-based authorization
- Refresh tokens
- Rate limiting
- Health checks with monitoring integrations

---

# Author
Precious Emmanuel
Developed as part of a backend engineering assessment demonstrating production-grade API development practices using ASP.NET Core and modern backend architecture principles.
