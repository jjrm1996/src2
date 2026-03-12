# Microservices Solution

This solution consists of four microservices: Products, Customers, Sales, and Search. Each service is designed to operate independently, following a microservices architecture that avoids direct dependencies between them.

## Project Structure

- **Products**: Manages product information.
  - **Data**: Contains the `ProductRepository` for CRUD operations.
  - **Models**: Defines the `Product` class.
  - **Presentation**: Implements minimal API endpoints for product management.
  
- **Customers**: Manages customer information.
  - **Data**: Contains the `CustomerRepository` for CRUD operations.
  - **Models**: Defines the `Customer` class.
  - **Presentation**: Implements minimal API endpoints for customer management.
  
- **Sales**: Manages sales transactions.
  - **Data**: Contains the `SalesRepository` for CRUD operations.
  - **Models**: Defines the `Sale` class.
  - **Presentation**: Implements minimal API endpoints for sales management.
  
- **Search**: Provides search functionality across products, customers, and sales.
  - **Data**: Contains the `SearchRepository` for searching operations.
  - **Models**: Defines the `SearchQuery` class.
  - **Presentation**: Implements minimal API endpoints for search functionality.

## Inter-Project Communication

The services communicate with each other through HTTP requests, utilizing the `IHttpClientFactory` for making calls to other services. The configuration for these endpoints is stored in the `config/services.json` file.

## Running the Solution

To run the solution, ensure you have the latest version of .NET 10 installed. You can build and run each microservice independently. Each service has its own configuration file (`appsettings.json`) for specific settings.

## Conclusion

This microservices architecture allows for scalability and maintainability, enabling each service to evolve independently while providing a cohesive system for managing products, customers, sales, and search functionalities.