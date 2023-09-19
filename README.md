# Task Management API

This Task Management API was built using ASP.NET Core, designed to manage tasks, projects, users, and notifications. It follows SOLID principles and separates concerns effectively.

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

# Task Endpoints
1. ```bash
   POST /Task/Create: Create a new task.
   
   GET /Task/Get: Retrieve a list of tasks.
   GET /Task/Get/{id}: Retrieve a specific task by ID.
   GET /Task/DueDate: Retrieve tasks due date.
   
   PUT /Task/Get/{id}: Update a task by ID.
   PUT /Task/Assignment: Assign a task to a project.
   PUT /Task/UnAssignment/{id}: UnAssign task to a project.
   
   DELETE /Task/Update/{id}: Delete a task by ID.

# User Endpoints
2. ```
   POST /User/Create: Create a new user.
   GET /User/Get: Retrieve a list of users.
   GET /User/Get/{id}: Retrieve a specific user by ID.
   PUT /User/Update/{id}: Update a user by ID.
   DELETE /User/Delete/{id}: Delete a user by ID.
   
# Project Endpoints
3. ```
   POST /Project/Create: Create a new project.
   GET /Project/Get: Retrieve a list of projects.
   GET /Project/Get/{id}: Retrieve a specific project by ID.
   PUT /Project/Update/{id}: Update a project by ID.
   DELETE /Project/Delete/{id}: Delete a project by ID.

# Notification Endpoints
4. ```
    GET /Notification/Get: Retrieve a list of notifications.
    GET /Notification/Get/{id}: Retrieve a specific notification by ID.
   DELETE /Notification/Delete/{id}: Delete a notification by ID.

In the Notification Endpoint, I omitted a create method because notifications are automatically generated when a user is assigned or unassigned from a task.

You can access the Swagger UI by visiting /swagger in your web browser after running the API for additional endpoints and detailed API documentation.

