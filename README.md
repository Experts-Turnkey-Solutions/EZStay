# EZStay
![Screenshot 2025-05-16 144119](https://github.com/user-attachments/assets/0321f21e-0b1e-4d40-a400-01daf46a33c1)
![Screenshot 2025-05-16 144139](https://github.com/user-attachments/assets/d209d32d-5bf4-47f8-80d5-d6b995675a68)

EZStay is a powerful property booking and management platform built with ASP.NET. Its intuitive design includes a user-friendly web interface and a robust RESTful API, making integration with external systems seamless.

---

## 🏠 Overview
EZStay streamlines the process of listing, managing, and booking rental properties. It offers:
- **Role-based access control**
- **Booking workflows**
- **Property management**
- **User registration and comprehensive API endpoints**

---

## 🏗️ Architecture
The project adheres to **Clean Architecture** principles, ensuring scalable and maintainable code. It is structured into:

### **Main Components:**
1. **EZStay.API**  
   - An ASP.NET Core backend implementing RESTful endpoints.
2. **EZStay.UI**  
   - ASP.NET MVC frontend for user interaction.
3. **Domain Models**  
   - Core entities like `User`, `Property`, and `Booking`.
4. **Services**  
   - Encapsulates business logic.
5. **Repositories**  
   - Abstracts data access details.
6. **DTOs (Data Transfer Objects)**  
   - Manages data exchange between API and clients.

---

## 🔧 Tech Stack
- **Backend:** ASP.NET Core  
- **Frontend:** ASP.NET MVC powered by Razor Views and jQuery  
- **Database:** SQL Server with Entity Framework Core  
- **API Documentation:** Swagger / OpenAPI  
- **Authentication:** Role-based JWT (JSON Web Token)

---

## 📋 Features

### **🔐 User Management**
- User registration and authentication.
- Role-based access control (e.g., Admin, Manager, Owner, etc.).
- User profile updates.

### **🏡 Property Management**
- Listing properties with image uploads.
- Advanced filtering and search.
- Admin approvals for property listings.

### **📅 Booking System**
- Create, update, cancel property bookings.
- Track booking statuses.
- View booking history.

### **📊 Admin Dashboard**
- Manage system parameters (users, bookings, properties).
- Configure platform settings.  

---

## 🔌 API Endpoints Overview
Base URL: `https://localhost:7301/api/v{version}/`

### **Authentication**
- `POST /Auth/register` - Register a new user  
- `POST /Auth/login` - Authenticate and get a JWT  

### **User**
- `GET /User` - List all users  
- `GET /User/{id}` - Fetch user details by ID  
- `PUT /User/{id}` - Update user details  
- `DELETE /User/{id}` - Delete a user  

### **Property**
- `GET /Property` - List all properties  
- `POST /Property` - Add new property  
- `PUT /Property/{id}` - Update property   
- `DELETE /Property/{id}` - Delete property  

### **Booking**  
- `POST /Booking` - Create a booking  
- `GET /Booking` - View all bookings  
- `PUT /Booking/{id}` - Update booking  
- `DELETE /Booking/{id}` - Cancel booking  

---

## 🔄 Development Workflow

1. Create a feature branch from `master`.  
2. Write and test new changes.  
3. Commit with descriptive messages.  
4. Submit a Pull Request for code review.  
5. Merge changes with approval.

---

## 🔐 User Roles
The app supports five main roles:

| Role                | Permissions                              |
|---------------------|------------------------------------------|
| **Admin**           | Full access to manage all platform features. |
| **Manager**         | Oversee properties and bookings.         |
| **AccountManager**  | Control user account settings.           |
| **ContentManager**  | Moderate property content and descriptions. |
| **Owner**           | Manage their own properties and bookings. |
| **User**            | Browse properties and make bookings.     |

---

## 🚀 Getting Started

### **Prerequisites**
- Install `.NET 6 SDK` or later.
- Install `SQL Server` (LocalDB or equivalent).
- Recommended: Visual Studio 2022 or Visual Studio Code.

### **Setup Instructions**
1. Clone the repository:
```bash
git clone https://github.com/Experts-TurnKey-Solutions/EZStay.git
cd EZStay

Setup the database:

Open the Package Manager Console in Visual Studio.
Run:
bash
Copy
Update-Database
Start the services:

Run EZStay.API on https://localhost:7301.
Run EZStay.UI on https://localhost:5001.
Access the application:

Web UI: https://localhost:5001
Swagger API Docs: https://localhost:7301/index.html
📁 Folder Structure
plaintext
Copy
EZStay/
├── EZStay.API/             
│   ├── Controllers/       # REST API endpoints
│   └── Program.cs
├── EZStay.UI/             
│   ├── Views/             # Razor Views (Frontend)
│   └── wwwroot/           # Static files
├── Models/                # Domain Models (Business Entities)
├── DTOs/                  # Data Transfer Objects
├── Services/              # Business Logic Layer
├── Repositories/          # Data Repositories
├── Middleware/            # Exception Handling Middleware
└── Utils/                 # Utilities (Security Helpers, Role Mappings)
📜 License
All rights reserved © 2025 - EZStay.UI

🤝 Contributors
@yazansedih – Project Lead
Copy
---

### To Convert to a PDF:
1. **Use Markdown to PDF tools:**
   - Copy the Markdown content into a Markdown editor like [VSCode](https://code.visualstudio.com/).
   - Use extensions like `Markdown PDF` to export a PDF.

2. **Online Markdown Converters:**
   - Paste the content into [Dillinger](https://dillinger.io/) or an online Markdown editor.
   - Export to PDF directly.

Would you like any further assistance with converting or adjustments?

[EZStay.pdf](https://github.com/user-attachments/files/20243445/EZStay.pdf)

