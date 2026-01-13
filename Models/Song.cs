public class Song
{
    public int Id { get; set; }

    public string Title { get; set; }
    public string Artist { get; set; }

    public string FilePath { get; set; }   // where MP3 is stored
    public string CoverImage { get; set; } // optional album cover

    public DateTime UploadDate { get; set; } = DateTime.Now;
}
