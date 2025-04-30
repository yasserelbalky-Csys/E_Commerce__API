# E-Commerce MVC Application

## Overview

The **E-Commerce MVC Application** is a web-based platform designed to facilitate online shopping. It provides features for user authentication, product browsing, shopping cart management, order placement, and payment processing. The application is built using modern web technologies and follows the Razor Pages architecture for simplicity and maintainability.

---

## Features

### User Features

- **User Registration and Login**: Secure user authentication using ASP.NET Core Identity.
- **Role-Based Access Control**: Admin and User roles with specific permissions.
- **Product Browsing**: View products by categories, subcategories, and brands.
- **Shopping Cart**: Add, update, or remove items from the cart.
- **Order Management**: Place orders and view order history.
- **Payment Integration**: Secure payment processing using Stripe.
- **Two-Factor Authentication (2FA)**: Enhanced account security with recovery codes.

### Admin Features

- **Product Management**: Add, update, or delete products.
- **Category Management**: Manage categories and subcategories.
- **Order Management**: View and update order statuses.
- **User Management**: Assign roles and manage user accounts.

---

## Technologies Used

### Backend

- **ASP.NET Core Razor Pages**: For server-side rendering and page management.
- **Entity Framework Core**: ORM for database operations.
- **ASP.NET Core Identity**: For user authentication and role management.
- **Stripe API**: For secure payment processing.
- **JWT (JSON Web Tokens)**: For token-based authentication.
- **Dependency Injection**: For service management and loose coupling.

### Frontend

- **Bootstrap 5**: For responsive and mobile-first design.
- **jQuery Validation**: For client-side form validation.
- **Font Awesome**: For icons and UI enhancements.

### Database

- **SQL Server**: Relational database for production.
- **SQLite**: Lightweight database for development and testing.

### Development Tools

- **Visual Studio 2022**: IDE for development.
- **Hot Reload**: Real-time updates during development.
- **LiveReload**: Automatic browser refresh for frontend changes.

---

## Installation

### Prerequisites

- .NET 8 SDK
- SQL Server or SQLite
- Visual Studio 2022

### Steps

1. Clone the repository:
2. Restore NuGet packages:
3. Update the database connection string in `appsettings.json`:
4. Apply database migrations:
5. Run the application:
6. Open the application in your browser at `http://localhost:5000`.

---

## Usage

### For Users

1. **Register**: Create an account using the registration page.
2. **Login**: Log in with your credentials.
3. **Browse Products**: Navigate through categories and subcategories.
4. **Add to Cart**: Add products to your shopping cart.
5. **Checkout**: Place an order and make a payment.
6. **View Orders**: Check your order history.

### For Admins

1. **Login as Admin**: Use admin credentials to log in.
2. **Manage Products**: Add, edit, or delete products.
3. **Manage Categories**: Organize products into categories and subcategories.
4. **Manage Orders**: View and update order statuses.
5. **Manage Users**: Assign roles and manage user accounts.

---

## API Services

### Order Service

- Fetch all orders.
- Fetch order by ID.
- Create, update, and delete orders.
- Fetch order details.

### SubCategory Service

- Fetch all subcategories.
- Fetch subcategory by ID.
- Create, update, and delete subcategories.

---

## Security Features

- **Password Hashing**: Secure storage of user passwords.
- **Two-Factor Authentication (2FA)**: Additional layer of security.
- **JWT Authentication**: Secure token-based authentication.
- **Role-Based Access Control**: Restrict access based on user roles.

---

## Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository.
2. Create a new branch:
3. Commit your changes:
4. Push to the branch:
5. Open a pull request.

---

## License

This project is licensed under the [MIT License](LICENSE).

---

## Contact

For any questions or support, please contact:

- **Email**: support@ecommerce.com
- **Website**: [www.ecommerce.com](http://www.ecommerce.com)
