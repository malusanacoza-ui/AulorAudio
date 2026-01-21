# Requirements — AuloraAudio

## Stakeholders
- **Listeners (Users):** browse music, search, stream, download, manage playlists.
- **Administrators:** upload songs, manage artists, genres, and content.
- **System Administrator:** manage users, roles, and system configuration (ASP.NET Identity).

---

## Functional Requirements
- Register and login
- Browse all available songs
- Search songs by title, artist, or genre
- Stream songs using an in-browser audio player
- Download songs for offline use
- View song details (cover image, artist, genre)
- Manage playlists (optional enhancement)
- Admin can add, edit, and delete songs
- Admin can upload audio files and cover images

### System Rules
- Only authenticated users can stream or download songs
- Only users with the **Admin** role can manage songs
- Uploaded files must be valid audio/image formats
- Audio and image file paths must be web-accessible
- Songs removed by admin are no longer available to users

---

## Non-Functional Requirements
- **Security:** ASP.NET Core Identity, role-based authorization, CSRF protection
- **Performance:** fast audio streaming, optimized queries, pagination for song lists
- **Scalability:** stateless web application, database-backed storage
- **Reliability:** consistent file storage and database synchronization
- **Observability:** logging of user actions and admin operations
- **Maintainability:** MVC separation of concerns, DTO usage, clean folder structure
- **Testability:** unit-testable controllers and services; validation coverage
- **Usability:** responsive UI and accessible navigation

---

## Acceptance Criteria
- A user can successfully register and log in
- A user can search and play songs in the browser
- Audio files play correctly using valid paths
- Admin users can upload songs with cover images
- Non-admin users cannot access admin functionality
- Songs appear immediately after being added
- Downloaded songs match the uploaded audio files
- Application runs without errors and serves static files correctly
