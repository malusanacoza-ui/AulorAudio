using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AulorAudio.Models;

namespace AulorAudio.Controllers
{
    public class SongsController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public SongsController(IWebHostEnvironment env)
        {
            _env = env;
        }

        // ============================
        // GET: Songs (PUBLIC)
        // ============================
        public IActionResult Index()
        {
            var musicDir = Path.Combine(_env.WebRootPath, "music");

            if (!Directory.Exists(musicDir))
                return View(new List<Song>());

            var songs = Directory.GetFiles(musicDir, "*.mp3")
                .Select((file, index) =>
                {
                    var fileName = Path.GetFileName(file);

                    return new Song
                    {
                        Id = index + 1,
                        Title = Path.GetFileNameWithoutExtension(file),
                        Artist = "Unknown Artist",
                        FilePath = "/music/" + fileName,
                        CoverImage = "/images/default-cover.jpg" // fixed cover
                    };
                })
                .ToList();


            return View(songs);

        }

        // ============================
        // DOWNLOAD (LOGIN REQUIRED)
        // ============================
        [Authorize]
        public IActionResult Download(string file)
        {
            if (string.IsNullOrWhiteSpace(file))
                return BadRequest();

            // Prevent path traversal
            file = Path.GetFileName(file);

            var path = Path.Combine(_env.WebRootPath, "music", file);

            if (!System.IO.File.Exists(path))
                return NotFound();

            return PhysicalFile(path, "audio/mpeg", file);
        }
    }
}
