# AulorAudio — System Architecture

## Architecture Pattern
- **ASP.NET Core MVC (Model–View–Controller)**
  - **Model:** Entity Framework Core models representing Songs, Users, Genres, Playlists, and related entities.
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
  - Playlist creation and management
  - Search functionality
  - Streaming and download logic
- **Validation:** Ensures correct input before data persistence.

### Data Access Layer
- **Entity Framework Core**
  - Handles database CRUD operations.
  - Uses `DbContext` to manage songs, users, playlists, and genres.
- **Database:** SQL Server for structured data storage.

---

## Authentication & Authorization
- **ASP.NET Core Identity**
  - User registration and login
  - Role-based authorization:
    - **Admin:** can manage songs and content.
    - **User:** can browse, stream, and download songs.
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

## Optional Enhancements
- Caching popular songs for faster streaming
- Asynchronous loading of song lists
- Logging user activity with Serilog
- API endpoints for future mobile apps

---

# Entity Relationship Diagram — AulorAudio

```mermaid
erDiagram
    USER ||--o{ PLAYLIST : owns
    USER ||--o{ SONG_DOWNLOAD : downloads
    SONG }o--|| GENRE : belongs_to
    SONG ||--o{ PLAYLIST_ITEM : included_in
    PLAYLIST ||--o{ PLAYLIST_ITEM : contains

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

    GENRE {
        int Id
        string Name
    }

    PLAYLIST {
        int Id
        string Name
        string UserId
    }

    PLAYLIST_ITEM {
        int Id
        int PlaylistId
        int SongId
    }

    SONG_DOWNLOAD {
        int Id
        int UserId
        int SongId
        datetime DownloadedAt
    }
