# Exam System

A comprehensive exam management system built with ASP.NET Core 8.0, implementing CQRS pattern with MediatR, Entity Framework Core, and JWT authentication.

## 📋 Features

- User authentication and authorization with JWT
- Exam creation and management
- Question bank with multiple choice support
- Attempt tracking with shuffled questions/choices
- Role-based access (Admin, Teacher, Student)
- Email verification system
- Image upload for questions and exams

## 🏗️ Architecture

- **Pattern:** CQRS (Command Query Responsibility Segregation)
- **Framework:** ASP.NET Core 8.0
- **ORM:** Entity Framework Core 9.0
- **Authentication:** JWT Bearer tokens
- **Validation:** FluentValidation
- **Password Hashing:** BCrypt.Net

## 📊 Code Review

A comprehensive code review has been completed. See [CODE_REVIEW.md](./CODE_REVIEW.md) for detailed findings.

### Quick Summary:
- ✅ **Strengths:** Clean CQRS architecture, proper validation, BCrypt password hashing
- 🔴 **Critical Issues:** Exposed secrets, SQL injection risk, async anti-patterns
- 🟠 **High Priority:** Missing authorization, weak JWT config, file upload validation
- 🟡 **Medium Priority:** Missing logging, no tests, inconsistent error handling

### Immediate Action Required:
1. Remove exposed secrets from repository and rotate credentials
2. Fix SQL execution in UserRepository to use EF Core properly
3. Fix async/await anti-patterns (`.Result` usage causes deadlocks)
4. Add `[Authorize]` attributes to protected endpoints
5. Implement structured logging throughout

## 🚀 Getting Started

### Prerequisites
- .NET 8.0 SDK
- SQL Server
- Visual Studio 2022 or VS Code

### Setup
1. Clone the repository
2. Update connection string in `appsettings.json` (use User Secrets for sensitive data)
3. Run database migrations:
   ```bash
   dotnet ef database update
   ```
4. Run the application:
   ```bash
   dotnet run --project "Exam System"
   ```
5. Navigate to `https://localhost:<port>/swagger` for API documentation

## 📁 Project Structure

```
Exam System/
├── Domain/              # Entities, Enums, Exceptions
├── Feature/             # CQRS Commands/Queries by feature
│   ├── User/           # User management
│   ├── Exam/           # Exam operations
│   ├── Question/       # Question management
│   └── Categories/     # Category management
├── Infrastructure/      # Data access, repositories
│   ├── Persistance/    # DbContext and configurations
│   └── Repositories/   # Repository implementations
└── Shared/             # Cross-cutting concerns
    ├── Middlewares/    # Exception handling
    ├── Services/       # Email, image handling
    └── Validation/     # FluentValidation setup
```

## 🔐 Security Notes

⚠️ **IMPORTANT:** This repository previously contained exposed secrets. If you cloned before the security fixes:
- All JWT keys and email credentials have been rotated
- Use User Secrets for local development
- Never commit `appsettings.json` with real credentials

## 🧪 Testing

Currently, no automated tests exist. This is a high-priority item for improvement. Planned test coverage:
- Unit tests for handlers and services
- Integration tests for API endpoints
- Validation rule tests

## 📝 License

[Add license information]

## 👥 Contributing

[Add contribution guidelines]

---

**Note:** This is an educational/portfolio project. See CODE_REVIEW.md for production readiness assessment.