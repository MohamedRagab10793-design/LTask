# Product Manager Frontend

This is an Angular 16 application for managing products with full CRUD operations.

## Features

✨ **Modern UI Design** - Beautiful, gradient-based design with smooth animations
📦 **Product Management** - Full CRUD operations (Create, Read, Update, Delete)
📄 **Pagination** - Efficient data loading with pagination support
🎨 **Responsive Design** - Works great on all screen sizes
⚡ **Real-time Updates** - Instant UI updates after data operations
🛡️ **Form Validation** - Client-side validation for data integrity

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

## Project Structure

```
src/
├── app/
│   ├── components/
│   │   ├── product-list/      # Product list with grid view
│   │   └── product-form/       # Product create/edit modal form
│   ├── models/
│   │   └── product.model.ts    # TypeScript interfaces and models
│   ├── services/
│   │   └── product.service.ts  # API service for HTTP requests
│   ├── app.component.*         # Root component
│   ├── app.module.ts           # Main module
│   └── app-routing.module.ts   # Routing configuration
├── assets/                     # Static assets
└── styles.css                  # Global styles

```

## Technologies Used

- **Angular 16** - Frontend framework
- **TypeScript** - Programming language
- **RxJS** - Reactive programming
- **HttpClient** - HTTP communication
- **Reactive Forms** - Form handling and validation

## Features Breakdown

### Product List
- Grid-based card layout
- Pagination controls
- Edit and delete actions
- Empty state for no products
- Loading states
- Error handling

### Product Form
- Modal-based form
- Create and edit modes
- Form validation
- Real-time validation feedback
- Smooth animations

### Styling
- Modern gradient designs
- Custom animations
- Hover effects
- Responsive layout
- Custom scrollbar
- Premium color palette

## Contributing

Feel free to submit issues and enhancement requests!

## License

MIT License
