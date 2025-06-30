# London Stock Exchange Trade API
A minimal .NET 8 Web API to receive trade notifications and expose average stock values in real time.

# Features
- Postgres-backed persistent trade data.
- RESTful endpoints with Swagger.
- Unit tests using MSTest + NSubstitute + FluentAssertions.

# Tech Stack
- .NET 8
- PostgreSQL
- Entity Framework Core
- NSubstitute + MSTest + FluentAssertions
- Swagger (OpenAPI)

# API Endpoints
| Method | Endpoint           | Description                       |
|--------|--------------------|-----------------------------------|
| POST   | `/trades`          | Record a new trade                |
| GET    | `/stock/{symbol}`  | Get average price of a stock      |
| GET    | `/stocks`          | Get all stock average prices      |
| POST   | `/stocks/range`    | Get prices for selected tickers   |

# Enhancements:
- Caching for improved performance and reduction of load on the DB.