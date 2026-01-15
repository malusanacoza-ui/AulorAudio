using AulorAudio.Data;
using AulorAudio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace AulorAudio.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public AdminController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Dashboard()
        {
            return View("~/Views/Admin backup/Dashboard.cshtml");
        }

        public IActionResult AddSong()
        {
            return View("~/Views/Admin backup/AddSong.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> AddSong(Song song, IFormFile songFile, IFormFile coverImage)
        {
            if (ModelState.IsValid)
            {
                if (songFile != null && songFile.Length > 0)
                {
                    var songFilePath = Path.Combine(_environment.WebRootPath, "songs", songFile.FileName);
                    using var stream = new FileStream(songFilePath, FileMode.Create);
                    await songFile.CopyToAsync(stream);
                    song.FilePath = songFilePath;
                }

                if (coverImage != null && coverImage.Length > 0)
                {
                    var coverImagePath = Path.Combine(_environment.WebRootPath, "covers", coverImage.FileName);
                    using var stream = new FileStream(coverImagePath, FileMode.Create);
                    await coverImage.CopyToAsync(stream);
                    song.CoverImage = coverImagePath;
                }

                _context.Add(song);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Dashboard));
            }

            return View("~/Views/Admin backup/AddSong.cshtml", song);
        }
    }
}
