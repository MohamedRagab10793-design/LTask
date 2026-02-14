# Product Manager Frontend

This is an Angular 16 application for managing products with full CRUD operations.



## Prerequisites

- Node.js (v14 or higher)
- Angular CLI 16.1.0
- .NET 8 Backend API running on `http://localhost:5253`

## Installation

1. Install dependencies:
```bash
npm install
```

## Development Server

Run the development server:
```bash
ng serve
```

Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.

## Build

Build the project for production:
```bash
ng build
```

The build artifacts will be stored in the `dist/` directory.

## API Configuration

The API endpoint is configured in `src/app/services/product.service.ts`:
- Default: `http://localhost:5253/v1/Product`

Make sure your backend API is running on this port before starting the frontend application.





