## PostgreSQL Database

[https://www.enterprisedb.com/downloads/postgres-postgresql-downloads](https://www.enterprisedb.com/downloads/postgres-postgresql-downloads)

## Setup Database

Run the following commands in your terminal.

```bash
# Connect to PostgreSQL
psql -U postgres

# Create database
CREATE DATABASE mediaid;

# Create a user (replace 'yourpassword' with your own)
CREATE USER mediaiduser WITH PASSWORD 'yourpassword';

# Grant privileges
GRANT ALL PRIVILEGES ON DATABASE mediaid TO mediaiduser;

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
```

**Note:** Replace the placeholder values with your actual database credentials. The `.env.example` file contains a template you can copy.

## Install Entity Framework CLI Tools

```bash
dotnet tool install --global dotnet-ef
```