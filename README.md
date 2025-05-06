E-Commerce Order Management API Documentation 
Overview 
This project is a RESTful Web API developed using ASP.NET Core, designed to manage orders in 
an e-commerce system. The API allows clients to perform full CRUD operations on orders and view 
associated products. 
Features 
- Create a new order with a list of products 
- Read all orders 
- Get detailed information of an order by ID 
- Update order status (Pending, Completed, Cancelled) 
- Delete an order 
- Filter orders by status 
- Calculate total amount using an extension method 
- Global rate limiting via middleware 
- Input validation using FluentValidation 
Tech Stack 
- ASP.NET Core Web API 
- Entity Framework Core (Code First) 
- SQL Server 
- FluentValidation 
- Swagger 
- Custom Middleware 
Models 
Order 
Product

-Used FluentValidation to ensure incoming data integrity: 
- Product name is required and must be within length limits. 
- Price and quantity must be positive numbers. 
- Order must contain at least one product. 
- Order status must be a valid enum value. 
Extension Method 
Implemented an extension method to calculate the TotalAmount of an order: 
Rate Limiting 
A custom middleware intercepts all requests to: 
- Apply a basic global rate limit. 
- Return a standardized JSON error response if the limit is exceeded. 
: 
dotnet ef database update 
dotnet run 
4. Use Swagger at: https://localhost:{port}/swagger for interactive API testing. 
