# AulorAudio — System Architecture

## Architecture Pattern
- **ASP.NET Core MVC (Model–View–Controller)**
  - **Model:** Entity Framework Core models representing Songs, Users, Favorites, Likes, and related entities.
  - **View:** Razor pages for UI (song lists, search, streaming player, downloads)
  - **Controller:** Handles user requests, invokes services, and returns views or JSON.

---

## Layers

### Presentation Layer
- **Views:** Razor pages for music browsing, searching, streaming, and downloads.
- **Controllers:** Handle routing, user requests, and authorization.
- **UI Components:** HTML, CSS, JavaScript, and responsive design.

### Application / Business Logic Layer
- **Services:** Handle operations such as:
  - Song management (add, edit, delete)
  - Favorite and like handling
  - Search functionality
  - Streaming and download logic
- **Validation:** Ensures correct input before data persistence.

### Data Access Layer
- **Entity Framework Core**
  - Handles database CRUD operations.
  - Uses `DbContext` to manage songs, users, playlists, favorites, and likes.
- **Database:** SQL Server for structured data storage.

---

## Authentication & Authorization
- **ASP.NET Core Identity**
  - User registration and login
  - Role-based authorization:
    - **Admin:** can manage songs and content.
    - **User:** can browse, stream, like, favorite, and download songs.
- **Security Measures:**
  - Password hashing
  - CSRF protection
  - Role checks in controllers

---

## File Storage
- **Music files:** stored in `wwwroot/music`
- **Cover images:** stored in `wwwroot/images`
- File paths are stored in the database as **web-accessible URLs**.

---

## High-Level Flow

1. User requests a page (e.g., Browse Songs)
2. Controller receives request
3. Controller calls Service Layer
4. Service Layer queries Database via EF Core
5. Data is returned to Controller
6. Controller renders View to User via Razor template
7. Audio streaming uses `<audio>` HTML element referencing `wwwroot/music` files

---


# Entity Relationship Diagram — AulorAudio

```mermaid
erDiagram
    USER ||--o{ FAVORITE_SONG : favorites
    USER ||--o{ SONG_LIKE : likes
    SONG ||--o{ FAVORITE_SONG : favorited_in
    SONG ||--o{ SONG_LIKE : liked_in

    USER {
        string Id
        string UserName
        string Email
        string Role
    }

    SONG {
        int Id
        string Title
        string Artist
        string FilePath
        string ImagePath
    }

    FAVORITE_SONG {
        int Id
        int UserId
        int SongId
        datetime FavoritedAt
    }

    SONG_LIKE {
        int Id
        int UserId
        int SongId
        datetime LikedAt
    }
