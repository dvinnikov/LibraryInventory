# Library Book Inventory

A simple web application for managing a library's book inventory. The application allows users to view, add, edit, and delete books. It also includes a Web API for performing CRUD operations on the inventory.

---

## Features

- Manage books with details like Title, Author, ISBN, Publication Year, Quantity, and Category.
- Add, edit, and delete book categories.
- View all books in a tabular format.
- Web API with pagination, search, and sorting for books.
- Backend powered by Entity Framework with Code First migrations.
- Unit tests for critical components using MSTest.

---

## Technologies Used

- **Frontend**: ASP.NET WebForms
- **Backend**: ASP.NET Web API
- **Database**: MS SQL Server
- **ORM**: Entity Framework (Code First)
- **Unit Testing**: MSTest
- **Programming Language**: C#

---

## Setup Instructions

### Prerequisites

1. **Software**:
   - Visual Studio 2022 (or compatible version with .NET Framework 4.8 support)
   - MS SQL Server
   - Git
2. **Dependencies**:
   - Entity Framework
   - MSTest

### Steps to Run the Project

1. **Clone the repository**:
   
bash
   git clone <https://github.com/dvinnikov/LibraryInventory.git>
   
2. **Open the project in Visual Studio.**:
3. **Restore NuGet packages:**:
   dotnet restore
4. **Configure the database:**:
   - Open the web.config file.
   - Update the DefaultConnection string:

   ```xml<connectionStrings>
   <add name="DefaultConnection" 
         connectionString="Server=YourServerName;Database=LibraryInventory;Trusted_Connection=True;" 
         providerName="System.Data.SqlClient" />
   </connectionStrings>
     
 Replace YourServerName with your SQL Server instance name.
  
5. **Apply migrations:**
- Open the Package Manager Console in Visual Studio.
- **Run the following command:** Update-Database

6. **Build and run the project:**
- Build the solution using Ctrl+Shift+B.
- Run the application using F5.
