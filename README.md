# Hospital Appointment System

A comprehensive web application for managing hospital appointments, patient records, and healthcare services. This system provides interfaces for patients, doctors, and administrators to streamline healthcare operations.

## Features

### For Patients
- Account registration and management
- Book, view, and cancel appointments
- View medical history and reports
- Submit feedback about services
- Live support chat
- FAQ section

### For Doctors
- Manage personal schedule and availability
- View and manage appointments
- Create and manage medical reports
- Access patient medical history
- Provide feedback on system

### For Administrators
- User management (patients, doctors, staff)
- Appointment oversight and management
- Doctor schedule management
- System reports and analytics
- Feedback management
- System configuration

## Technology Stack

### Backend
- **Framework**: ASP.NET Core Web API
- **Database**: SQL Server with Entity Framework Core
- **Authentication**: JWT (JSON Web Tokens)
- **Architecture**: N-tier architecture with:
  - Entities Layer
  - Data Access Layer
  - Business Logic Layer
  - API Layer
- **Documentation**: Swagger/OpenAPI

### Frontend
- **Framework**: Angular 18
- **UI Components**: Custom Angular components
- **State Management**: RxJS
- **Styling**: SCSS

## Project Structure

### Backend
```
Backend/
├── HospitalAppointmentSystem/
    ├── Business/           # Business logic and services
    ├── DataAccess/         # Database context and repositories
    ├── DTO/                # Data Transfer Objects
    ├── Entities/           # Domain models
    └── HospitalAppointmentSystem/ # API Controllers and configuration
```

### Frontend
```
Frontend/
├── HospitalAppointmentSystem/
    ├── src/
        ├── app/
            ├── admin/      # Admin dashboard and management
            ├── auth/       # Authentication components
            ├── doctor/     # Doctor dashboard and tools
            ├── patient/    # Patient dashboard and services
            └── shared/     # Shared components and services
```

## Getting Started

### Prerequisites
- .NET 8 SDK
- Node.js and npm
- SQL Server
- Angular CLI

### Backend Setup
1. Navigate to the Backend directory:
   ```
   cd Backend/HospitalAppointmentSystem
   ```
2. Restore NuGet packages:
   ```
   dotnet restore
   ```
3. Update the connection string in `appsettings.json` to point to your SQL Server instance
4. Apply database migrations:
   ```
   dotnet ef database update
   ```
5. Run the API:
   ```
   dotnet run
   ```
6. Access Swagger documentation at `https://localhost:7001/swagger`

### Frontend Setup
1. Navigate to the Frontend directory:
   ```
   cd Frontend/HospitalAppointmentSystem
   ```
2. Install dependencies:
   ```
   npm install
   ```
3. Update the API URL in the environment files if needed
4. Start the development server:
   ```
   npm start
   ```
5. Access the application at `http://localhost:4200`

## User Roles and Access

- **Patient**: Register, book appointments, view medical history
- **Doctor**: Manage schedule, handle appointments, create medical reports
- **Admin**: Full system access, user management, reporting

## Security Features

- JWT authentication
- Role-based authorization
- HTTPS enforcement
- Input validation
- Exception handling middleware

## Acknowledgments

- Built with modern web technologies
- Designed for optimal user experience
- Focused on healthcare efficiency and patient satisfaction 
