# LTask - Product Management Application

## Quick Start Guide

### Backend Setup (Already Complete)
Your .NET 8 backend API is already set up with CORS support for the Angular frontend.

### Frontend Setup

1. **Navigate to the frontend directory:**
   ```bash
   cd frontend
   ```

2. **Install dependencies (if not already done):**
   ```bash
   npm install
   ```

3. **Start the Angular development server:**
   ```bash
   ng serve
   ```
   
   The app will run at: `http://localhost:4200`

### Running the Full Application

1. **Start the Backend API:**
   - Open the solution in Visual Studio or Rider
   - Run the `LTaskAPI` project
   - The API will start at `http://localhost:5253`
   - Swagger UI will be available at `http://localhost:5253/swagger`

2. **Start the Frontend:**
   - Open a new terminal
   - Navigate to the `frontend` folder
   - Run `ng serve`
   - Open your browser to `http://localhost:4200`

### What You'll See

The Angular app features:
- ✨ **Beautiful Modern UI** - Gradient-based design with smooth animations
- 📦 **Product Grid** - Card-based product display
- ➕ **Add Products** - Modal form for creating new products
- ✏️ **Edit Products** - In-place editing with validation
- 🗑️ **Delete Products** - Confirmation before deletion
- 📄 **Pagination** - Navigate through large datasets
- 🎯 **Real-time Validation** - Form validation with helpful error messages

### API Endpoints

The frontend communicates with these backend endpoints:
- `GET /v1/Product` - Get all products (with pagination)
- `GET /v1/Product/{id}` - Get product by ID  
- `POST /v1/Product` - Create new product
- `PUT /v1/Product/{id}` - Update product
- `DELETE /v1/Product/{id}` - Delete product

### Technology Stack

**Backend:**
- .NET 8 Web API
- Entity Framework Core
- SQL Server
- FluentValidation
- AutoMapper
- Serilog

**Frontend:**
- Angular 16
- TypeScript
- RxJS
- Reactive Forms
- HttpClient

### Project Structure

```
LTask/
├── LTaskAPI/                 # Backend .NET API
│   ├── Controllers/
│   │   └── ProductController.cs
│   ├── Services/
│   ├── DTOs/
│   └── Program.cs           # CORS configuration added
└── frontend/                # Angular frontend
    └── src/
        ├── app/
        │   ├── components/
        │   │   ├── product-list/
        │   │   └── product-form/
        │   ├── models/
        │   ├── services/
        │   └── app.module.ts
        └── styles.css
```

### Notes

- CORS has been configured in the backend to allow requests from `http://localhost:4200`
- The frontend automatically connects to the backend at `http://localhost:5253`
- Both servers must be running for the application to work properly

Enjoy your new Product Management application! 🎉
