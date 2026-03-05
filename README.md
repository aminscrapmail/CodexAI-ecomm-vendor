# E-commerce Vendor Management (.NET 9 + Angular)

This repository contains:

- **Backend**: ASP.NET Core Web API (.NET 9) with Entity Framework Core (InMemory provider for local demo).
- **Frontend**: Angular app with login page and product listing management.

## Features

- Login page (`/login`)
- Product listing page (`/products`)
- Add product via modal
- Update product via modal
- Soft delete product
- Filter by text and category
- Product fields:
  - `id`
  - `name`
  - `category`
  - `description`
  - `stock`
  - `price`
  - `lastUpdated`
  - `isDeleted`
  - `modifiedBy`

## Backend setup

```bash
cd backend/EcommVendor.Api
dotnet restore
dotnet run
```

Default API endpoints:

- `POST /api/auth/login`
- `GET /api/products?search=&category=`
- `POST /api/products`
- `PUT /api/products/{id}`
- `DELETE /api/products/{id}?modifiedBy=`

## Frontend setup

```bash
cd frontend
npm install
npm run start
```

By default frontend expects API at `https://localhost:5001/api` (see `src/environments/environment.ts`).
