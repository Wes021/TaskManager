Task Management System

A scalable Task Management System built with ASP.NET Core Web API, following a Modular Monolith architecture and applying Domain-Driven Design (DDD) principles.

The system provides a structured platform for managing users, projects, tasks, task assignments, comments, task history, and notifications while maintaining clear separation between business modules and responsibilities.

The main goal of building this project is to practice system design and apply clean code principles.

1. Project Overview

The Task Management System is an enterprise-style backend application designed to manage the complete lifecycle of projects and tasks.

The project was developed to practice and demonstrate:

Domain-Driven Design
Modular Monolith architecture
Clean separation of responsibilities
Domain-driven business rules
Authentication and authorization
Repository and Unit of Work patterns
Scalable application architecture

The system is designed so that business logic is kept within the appropriate domain and application layers rather than being concentrated inside controllers.

2. Key Features
   
User Management:
  
- User creation and management
- User activation/deactivation
- Soft deletion
- User listing with pagination and filtering
- Role-based access control

Authentication:

- JWT-based authentication
- Login
- Current authenticated user information
- Secure authentication flow

Project Management:

- Create projects
- Update projects
- Retrieve projects
- Delete projects
- Project status management
- Add project members
- Remove project members

Task Management:
- Create tasks
- Update tasks
- Retrieve tasks
- Delete tasks
- Assign users to tasks
- Remove task members
- Task status management
- Controlled task status transitions
- Task comments
- Task history
- Task attachments

Notifications:
- Notification creation through internal application services
- Notification types
- Read/unread state
- Mark notification as read
- Mark all notifications as read
- Retrieve notifications
- Unread notification count
- Delete notifications


Engineering Features:
- Global exception handling
- Request validation
- API documentation through Swagger/OpenAPI


3. Architecture

The application follows a Modular Monolith architecture combined with Domain-Driven Design principles.

Instead of separating the system into multiple independently deployed microservices, the application is deployed as a single application while maintaining clear boundaries between business modules.

Each module is responsible for its own business logic and data access.

High-Level Architecture:
- ASP.NET Core Web API
- Identity Module
- Projects Module
- Tasks Module
- Notifications Module



Module Structure:
Each module follows a layered structure:

Module
│
├── Domain
│   ├── Entities
│   ├── Value Objects
│   ├── Domain Rules
│   └── Domain Services
│
├── Application
│   ├── Services
│   ├── Handlers
│   ├── DTOs
│   └── Interfaces
│
└── Infrastructure
    ├── DbContext
    ├── Repositories
    └── Persistence
    


Architectural Principles:

The project applies:
- Separation of concerns
- Dependency inversion
- Encapsulation
- SOLID principles
- Domain-driven business rules
- Dependency Injection
- Repository pattern
- Unit of Work pattern
- Modular boundaries

The objective is to keep business rules independent from infrastructure and presentation concerns.



4. Modules

Identity Module
Responsible for:

- Authentication
- Users
- Roles
- JWT tokens
- Authorization
- Current user context

Projects Module
Responsible for:

- Project lifecycle
- Project status
- Project members
- Project-related authorization rules


Tasks Module
Responsible for:

- Task lifecycle
- Task assignments
- Task status
- Status transition rules
- Comments
- History
- Attachments Managment

Notifications Module
Responsible for:

- Internal Notification creation
- External/Custom Notification creation
- Notification types
- Notification retrieval
- Read/unread state
- Deletion
- Notification lifecycle

Some Notifications are created internally by the application rather than exposing a public endpoint that, and some are made as an Endpoints for the client.


5. Technologies
Backend:
- C#
- ASP.NET Core Web API
- Entity Framework Core
- LINQ
- SQL Server
  
Architecture & Design:
- Modular Monolith
- Domain-Driven Design
- SOLID principles
- Repository Pattern
- Unit of Work Pattern
- Dependency Injection
  
Authentication & Security:
- ASP.NET Core Authentication
- JWT
- Role-Based Authorization

  
API & Development:
- RESTful APIs
- Swagger / OpenAPI
- AutoMapper
- Global exception handling

DevOps:
- Git
- GitHub


6. Authentication & Authorization

The application uses JWT Bearer Authentication to secure API endpoints.

After successful authentication, the client receives a JWT containing the necessary identity and authorization information.

Protected endpoints require a valid bearer token.

Client
   │
   │ Login
   ▼
Authentication
   │
   │ JWT
   ▼
Client
   │
   │ Authorization: Bearer <token>
   ▼
Protected API

Authorization:

Authorization is implemented using roles and application-level permission checks.

The system supports role-based access such as:

SuperAdmin
Manager / Leader
Employee

Authorization rules are enforced at the appropriate application/domain boundaries rather than relying exclusively on controllers.



Project Status

The project is currently complete from a development and engineering perspective, but remains with hidden bugs and improvements that will be resolved weekly.

The implemented system includes:

Core business modules
Authentication and authorization
Domain-driven business rules
Notifications
Validation
Exception handling
Logging
Auditing
API documentation

The project will continue to receive regular maintenance, bug fixes, and improvements to further enhance its reliability, performance, and maintainability.

Note: Please note that this Readme is AI-generated and was checked for accuracy
