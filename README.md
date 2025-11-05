## PostgreSQL Database

[https://www.enterprisedb.com/downloads/postgres-postgresql-downloads](https://www.enterprisedb.com/downloads/postgres-postgresql-downloads)

## Setup Database

Run the following commands in your terminal.

```bash
# Connect to PostgreSQL
psql postgres

# Create database
CREATE DATABASE mediaid;

# Create a user (replace 'yourpassword' with your own)
CREATE USER mediaiduser WITH PASSWORD 'yourpassword';

# Grant privileges
GRANT ALL PRIVILEGES ON DATABASE mediaid TO mediaiduser;

# Exit
\q
```

## Install Entity Framework CLI Tools

```bash
dotnet tool install --global dotnet-ef
```