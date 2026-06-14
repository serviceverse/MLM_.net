# MLM Dashboard Application

A highly professional, responsive, and robust ASP.NET Core MVC application featuring a premium dashboard layout and a fully integrated Role-Based Access Control (RBAC) system. 

## Features
- **Premium UI/UX:** Glassmorphic login/registration pages and a responsive dashboard layout with a collapsible sidebar and top navigation.
- **Role-Based Access Control (RBAC):** Granular permission matrix allowing you to dynamically assign Create/Read/Update/Delete actions across different system modules to various user roles.
- **Entity Framework Core:** Fully configured database migrations and object-relational mapping.
- **Authentication:** JWT Bearer authentication system configured for secure sessions.

## Tech Stack
- **Framework:** .NET 10.0
- **Architecture:** MVC (Model-View-Controller)
- **Database:** Microsoft SQL Server
- **Frontend:** HTML5, CSS3, Bootstrap 5, Bootstrap Icons

---

## Getting Started

Follow these instructions to set up and run this project on a new local machine.

### 1. Install Prerequisites
Ensure you have the following installed on your target system:
* **.NET 10.0 SDK** (required to build and run the app)
* **Microsoft SQL Server** (or SQL Server Express) running locally
* **Git** 

### 2. Clone the Repository
Open your terminal or command prompt and clone the project:
```bash
git clone https://github.com/serviceverse/MLM_.net.git
cd MLM_.net/MLM
```

### 3. Restore Dependencies
.NET utilizes NuGet to manage packages. Running this command will read the `MLM.csproj` file and download all necessary libraries (like EF Core and JWT components):
```bash
dotnet restore
```

### 4. Setup the Database
Since local configuration files like `appsettings.Development.json` are excluded via `.gitignore` for security, you need to verify your local database connection.

1. Open `appsettings.json` and ensure your `ConnectionStrings` property correctly points to your local SQL Server instance.
2. If you don't have the Entity Framework CLI tools installed on the new machine, install them globally:
   ```bash
   dotnet tool install --global dotnet-ef
   ```
3. Run the database migrations to automatically build all required tables (Users, Roles, Modules, Actions, Permissions) inside your local SQL Server:
   ```bash
   dotnet ef database update
   ```

### 5. Run the Application
Finally, launch the application:
```bash
dotnet run
```
Navigate to the URL provided in the terminal output (e.g., `https://localhost:7073`) to view the application!
