## PostgreSQL Database

[https://www.enterprisedb.com/downloads/postgres-postgresql-downloads](https://www.enterprisedb.com/downloads/postgres-postgresql-downloads)

## Setup Database

Run the following commands in your terminal.

```bash
# Connect to PostgreSQL
psql -U postgres

# Create database
CREATE DATABASE mediaid;

# Exit
\q
```

## Setup Environment Variables

Create a `.env` file in the root directory of the project with the following variables:

```env
DB_HOST=yourhost
DB_PORT=5432
DB_NAME=mediaid
DB_USERNAME=mediaiduser
DB_PASSWORD=yourpassword
SECRET_KEY=your-secret-key-for-password-hashing
JWT_SECRET_KEY=your-jwt-secret-key-minimum-32-characters-long
JWT_EXPIRATION_MINUTES=60
```

**Note:** Replace the placeholder values with your actual database credentials. The `.env.example` file contains a template you can copy.

## Install Entity Framework CLI Tools

```bash
dotnet tool install --global dotnet-ef
```

## Migrate Models
```bash
# Create initial migration
dotnet ef migrations add InitialCreate

# Apply migration to database
dotnet ef database update

# Remove migration files
dotnet ef migrations remove

# Revert all migrations
dotnet ef database update 0
```
