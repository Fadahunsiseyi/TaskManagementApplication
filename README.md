# Task Management API

This is a Task Management API built using ASP.NET Core, designed to manage tasks, projects, users, and notifications. It follows SOLID principles and separates concerns effectively.

## Projects

The solution consists of the following projects:

- **API**: Contains controllers and exception middleware.
- **Application**: Contains interfaces for generic repository and services, error validation, DTO configurations, and dependency injection setup.
- **Common**: Contains DTOs (Notification DTO, Project DTO, Task DTO, and User DTO) and enums.
- **Domain**: Contains domain entities, including Base Entity, Notification Entity, Project Entity, Task Entity, and User Entity.
- **Persistence**: Houses the Application DbContext which utilizes Entity Framework Core and the Generic Repository.

## Getting Started

To set up and run the API on your local machine, follow these steps:

1. Clone this repository to your local environment:

   ```bash
   git clone [https://github.com/your-repo-url.git](https://github.com/Fadahunsiseyi/TaskManagementApplication.git)https://github.com/Fadahunsiseyi/TaskManagementApplication.git
2. Open the solution in Visual Studio.
3. Configure the database connection string in appsettings.json under the API project. This project uses SQLite, and the SQLite database is created automatically when the application starts. The database file is located in the TaskManagement.API folder.
4. Build the solution.
5. Run the API project.

## API ENDPOINTS
