# 🛒 WebApiShop — .NET 9

A comprehensive **RESTful Web API** for online shop management, built with **C#** and **.NET 9**. The project follows **Clean Architecture** principles and modern backend development best practices.

---

## 🏗️ Architecture & Layers

The project is structured into distinct layers to ensure **Separation of Concerns**, making the code readable, testable, and maintainable:

| Layer | Responsibility |
|---|---|
| **WebApiShop (API)** | Entry point, Controllers, Middleware, and Configurations |
| **Services** | Business logic implementation and process orchestration |
| **Repositories** | Data access abstraction and database interaction |
| **Dtos** | Data Transfer Objects to decouple the domain model from the API |
| **wwwroot** | Basic frontend interface built with JavaScript and HTML |

---

## 🔑 Key Features

### ✅ Identity & Security
Complete **User Management** (Login/Register) system, including a custom **Password Strength Validation** mechanism to ensure data security.

### 🗄️ Entity Framework Core (SQL Server)
Database management is handled via **EF Core**, providing a strongly-typed, LINQ-based interface to SQL Server.

### ⚡ Asynchronous Data Access
All database operations are implemented using the `async/await` pattern, optimizing thread usage and improving system scalability under heavy load.

### 📦 DTOs & AutoMapper
Usage of **Data Transfer Objects** to prevent circular dependencies and shield internal models. Mapping is performed automatically using **AutoMapper**.

### 🛡️ Centralized Error Handling
A global **Error Handling Middleware** intercepts exceptions across the application, ensuring consistent API responses and preventing sensitive data leaks.

### 📋 Structured Logging (NLog)
Integrated **NLog** for detailed event and error tracking across all layers, facilitating rapid monitoring and debugging in production environments.

### 📊 Traffic Auditing & Ratings
A dedicated **Rating system** tracks product feedback and monitors interaction trends within the database.

---

## 🛠️ Tech Stack

* **.NET 9 / C#** – Core framework and language.
* **ASP.NET Core Web API** – REST API hosting.
* **Entity Framework Core** – ORM for data access.
* **AutoMapper** – Object-to-object mapping.
* **NLog** – Structured logging.
* **Swagger (OpenAPI)** – API documentation and testing.

---

## 🚀 Getting Started

### Prerequisites
* [.NET 9 SDK](https://dotnet.microsoft.com/download)
* SQL Server (installed and running).

### Installation & Execution

1. Clone the repository.
2. Update the **Connection String** in the `appsettings.json` file.
3. Restore NuGet packages using the following command:
     dotnet restore
4. Build the solution:
     dotnet build
5. Run the application:
    dotnet run


    
6. The API will be available at:
   - HTTP: `http://localhost:5042`
   - HTTPS: `https://localhost:7017`

7. Access Swagger UI for API documentation at `/swagger`.

---

## 📂 Project Structure

| Folder | Description |
|---|---|
| **WebApiShop** | API entry point, middleware, and configurations |
| **Services** | Business logic and service interfaces |
| **Repositories** | Data access layer |
| **Dtos** | Data Transfer Objects |
| **wwwroot** | Static files (JavaScript, HTML) |

---

## 🧪 Testing

- Use the Swagger UI to test API endpoints.
- Unit tests can be added to validate business logic.

---

## 📜 License

This project is for educational and demonstration purposes.
