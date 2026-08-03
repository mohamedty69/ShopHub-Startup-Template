<h1 align="center">
  <br>
  AuraShop
  <br>
</h1>

<h4 align="center">A Modern, N-Tier ASP.NET Core MVC E-Commerce Platform.</h4>

<p align="center">
  <a href="#key-features">Key Features</a> •
  <a href="#architecture">Architecture</a> •
  <a href="#technologies-used">Technologies Used</a> •
  <a href="#installation-and-setup">Installation</a> •
  <a href="#database-configuration">Database Configuration</a>
</p>

<hr>

## 🚀 Overview

**AuraShop** (formerly ShopHub) is a fully-featured, enterprise-grade E-Commerce platform built using **ASP.NET Core MVC**. It features a clean, responsive storefront for customers and a powerful, glassmorphism-styled Admin Dashboard for store managers. 

The project strictly follows the **N-Tier Architecture** and **Repository Pattern** to separate concerns, making the codebase highly maintainable, scalable, and testable.

## ✨ Key Features

### 🛍️ Customer Storefront
* **Dynamic Product Catalog**: Browse products beautifully presented in responsive cards.
* **AJAX Shopping Cart**: Add, remove, and adjust item quantities in real-time without reloading the page.
* **Dynamic Cart Badge**: Cart icon automatically tracks the total number of items.
* **Modern Premium UI**: Built with a sleek, glassmorphic aesthetic, custom gradients, and smooth hover animations.

### 🛡️ Admin Dashboard (AuraShop Admin)
* **Secure Authentication**: Built-in ASP.NET Core Identity for robust login, registration, and role management.
* **Category & Product Management**: Full CRUD operations for managing the store's inventory.
* **User & Role Management**: Admins can lock/unlock user accounts, edit details, and assign roles.
* **DataTables Integration**: High-performance, searchable, and sortable tables for data management.

## 🏗️ Architecture

The solution is divided into distinct layers (N-Tier Architecture) to enforce the separation of concerns:

1. **`myshop.Web` (Presentation Layer)**: Contains the ASP.NET Core MVC Controllers, Views (Razor), CSS (Glassmorphism theme), and JavaScript (AJAX).
2. **`myshop.BLL` (Business Logic Layer)**: Contains Services, Data Transfer Objects (DTOs), and interfaces (`ICartService`, etc.). Encapsulates all business rules.
3. **`myshop.DAL` (Data Access Layer)**: Contains the Entity Framework Core `DbContext`, Repositories, and the `UnitOfWork` pattern for database transactions.
4. **`myshop.Entities` (Domain Layer)**: Contains the core database models/entities.

## 💻 Technologies Used

* **Framework**: .NET 8 / ASP.NET Core MVC
* **ORM**: Entity Framework Core
* **Database**: Microsoft SQL Server
* **Authentication**: ASP.NET Core Identity
* **Frontend**: Vanilla CSS (Custom Glassmorphism Theme), Bootstrap 5, FontAwesome 6
* **Interactivity**: jQuery, AJAX, DataTables
* **Notifications**: SweetAlert2 & Toastr

## 🛠️ Installation and Setup

### Prerequisites
* [.NET SDK](https://dotnet.microsoft.com/download) (latest version)
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express or Developer)
* Visual Studio or Visual Studio Code

### Database Configuration

1. Open `myshop.Web/appsettings.json`.
2. Locate the `ConnectionStrings` section and update the `DefaultConnection` to point to your local SQL Server instance.

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=AuraShopDB;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False"
}
```

### Applying Migrations

Open your terminal (or Package Manager Console in Visual Studio) and run the following command to create the database:

**Using .NET CLI:**
```bash
dotnet ef database update --project myshop.DAL --startup-project myshop.Web
```
**Using Package Manager Console:**
```powershell
Update-Database
```

### Running the Project

1. Set `myshop.Web` as the startup project.
2. Run the application (press `F5` in Visual Studio or `dotnet run` in the terminal).
3. Register a new user and enjoy the AuraShop experience!

---

> **Note**: This project was originally designed as a startup template but has been heavily upgraded with advanced AJAX cart functionalities, real-time UI updates, and a custom premium Glassmorphism design system.
