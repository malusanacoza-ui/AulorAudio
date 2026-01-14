using System.ComponentModel.DataAnnotations;
using AulorAudio.Models;

public class SongLike
{
    public int Id { get; set; }
    public string SongFile { get; set; }
    public string UserId { get; set; }
}


