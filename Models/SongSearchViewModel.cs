namespace AulorAudio.Models
{
    public class SongSearchViewModel
    {
        public string Query { get; set; }
        public List<Song> Songs { get; set; } = new();
    }
}
