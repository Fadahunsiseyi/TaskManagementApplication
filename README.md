# Task Management System Backend API

## Project Objective

Your task is to create a backend API for a Task Management System. This API will enable users to perform various actions, including creating, managing, and tracking tasks and projects, as well as receiving notifications.

## Project Requirements

### Data Entities & Relationships

- **Task**: Represents a task with attributes like title, description, due date, priority (e.g., low, medium, high), and status (e.g., pending, in-progress, completed).

- **Project**: Tasks can be organized into projects, each having a name, description, and a list of associated tasks.

- **User**: Users can create and manage tasks. Users have basic information such as name and email, along with a list of tasks they've created.

- **Notification**: Notifications inform users about events like upcoming due dates or task status changes. Each notification has a type (e.g., due date reminder, status update), message, and timestamp.

### API Features

- Implement CRUD operations for Tasks, Projects, Users, and Notifications.

- Create an endpoint to fetch tasks based on their status or priority.

- Add an endpoint to retrieve tasks due for the current week.

- Include an endpoint to assign or remove a task from a project.

- Provide an endpoint to mark a notification as read or unread.

### Notifications

Users should receive notifications when:

- A task's due date is within 48 hours.

- A task they created is marked as completed.

- They are assigned a new task.

### Architecture and Principles

- Organize your application using a clean architecture with separate layers (e.g., Presentation, Application, Domain, Infrastructure).

- Utilize Dependency Injection as needed.

- (Optional) Consider Domain-Driven Design (DDD) principles, including aggregates, entities, and value objects.

- (Optional) Implement domain events for significant actions (e.g., updating a task's due date).

- Follow SOLID principles to maintain a solid object-oriented design.

### Error Handling

- Implement error handling to return appropriate HTTP status codes and error messages for different scenarios, including validation errors and not found errors.

### Documentation

- Create a detailed README that explains how to set up, run, and test your API.

- Share any design decisions or assumptions you made during the project.

## Submission and Evaluation

When submitting your work, it will be evaluated based on the following criteria:

- Adherence to the specified project requirements.

- Implementation of a clean architecture with proper separation of concerns.

- Code quality, including readability, maintainability, and organization.

- Effective error handling and consideration of edge cases.

- (Optional) Use of Domain-Driven Design (DDD) and adherence to SOLID principles.

