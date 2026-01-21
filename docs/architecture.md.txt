# System Architecture — Music Streaming Web Application

## Architecture Pattern
- **ASP.NET Core MVC (Model–View–Controller)**
  - **Model:** Entity Framework Core models representing Songs, Users, Genres, etc.
  - **View:** Razor pages for UI (song lists, search, streaming player)
  - **Controller:** Handles user requests, invokes services, and returns views or JSON

---

## Layers

### Presentation Layer
- **Views:** Razor pages for music browsing, searching, streaming, and downloads
- **Controllers:** Handle routing, user requests, and authorization
- **UI Components:** HTML, CSS, JavaScript, and responsive design

### Application / Business Logic Layer
- **Services:** Handle operations like:
  - Song management (add, edit, delete)
  - Playlist handling
  - Search functionality
  - Streaming/download logic
- **Validation:** Ensures correct input before persistence

### Data Access Layer
- **Entity Framework Core**
  - Handles database CRUD operations
  - Uses `DbContext` to manage songs, users, playlists, and genres
- **Database:** SQL Server for structured storage

---

## Authentication & Authorization
- **ASP.NET Core Identity**
  - User registration and login
  - Role-based authorization:
    - **Admin:** can manage songs and content
    - **User:** can browse, stream, and download
- **Security Measures:**
  - Password hashing
  - CSRF protection
  - Role checks in controllers

---

## File Storage
- **Music files:** stored in `wwwroot/music` folder
- **Cover images:** stored in `wwwroot/images` folder
- File paths are saved in the database as **web-accessible URLs**  

---

## High-Level Flow

1. User requests a page (e.g., Browse Songs)
2. Controller receives request
3. Controller calls Service Layer
4. Service Layer queries Database via EF Core
5. Data returned to Controller
6. Controller returns View to User with Razor template
7. Audio streaming uses `<audio>` HTML element referencing `wwwroot/music` files

---

## Optional Enhancements
- Caching popular songs for faster streaming
- Asynchronous loading of song lists
- Logging user activity with Serilog
- API endpoints for future mobile apps
