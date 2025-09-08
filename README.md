# WorkSphere  

WorkSphere is a modern **Employee Management System** built with **ASP.NET Core 8.0 MVC**.  
It provides organizations with a centralized platform to manage employees, departments, projects, clients, assignments, and invoices in a structured and efficient manner.  

---

## Features  

- 🔐 **Secure Authentication** – Admin login/logout with ASP.NET Identity.  
- 👥 **Employee & Department Management** – Add, update, and organize employees and departments.  
- 📊 **Dashboard & Analytics** – Visual insights including employee distribution, project allocations, and client growth trends.  
- 📂 **Project & Assignment Tracking** – Manage projects and allocate employees to tasks.  
- 💼 **Client & Invoice Management** – Track clients, generate invoices, and manage invoice items.  
- 🎨 **Responsive UI** – Clean, minimal, and professional design with Bootstrap 5.  

---

## Preview  

### Dashboard  
![Dashboard Preview](./screenshots/dashboard.png)  

*(Dashboard preview of WorkSphere – Employee Management System)*  

---

## Tech Stack  

- **Framework:** ASP.NET Core 8.0 MVC  
- **Database:** Microsoft SQL Server with Entity Framework Core  
- **Authentication:** ASP.NET Core Identity (Admin role-based)  
- **Frontend:** Bootstrap 5, Chart.js for data visualization  
- **Language:** C#  

---

## Project Structure  

- **Models** – Data models including entities and view models  
- **Controllers** – Handles application logic and routes  
- **Views** – Razor pages for UI rendering  
- **Data** – AppDbContext and EF Core configurations  
- **wwwroot** – Static assets (CSS, JS, images)  

---

## Getting Started  

1. Clone the repository  
2. Update the connection string in `appsettings.json`  
3. Apply EF Core migrations to create the database  
4. Run the project with `dotnet run`  

The application will start at `https://localhost:5001` (or your configured port).  

---

## Note  

This project was built as a part of learning **.NET Entity Framework**.  

---

## License  

This project is licensed under the **MIT License** – you are free to use, modify, and distribute it with proper attribution.  
See the [LICENSE](./LICENSE) file for details.  

---
