namespace AulorAudio.Models
{
    public class Song
    {
        public int Id { get; set; }           // generated in-memory
        public string Title { get; set; }
        public string Artist { get; set; }
        public string FilePath { get; set; }
        public string CoverImage { get; set; }
    }
}
