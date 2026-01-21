# Entity Relationship Diagram — Music Streaming Web Application

## ERD Diagram

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
