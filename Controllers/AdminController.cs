using AulorAudio.Data;
using AulorAudio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AulorAudio.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public AdminController(
            ApplicationDbContext context,
            IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // ===============================
        // ADMIN DASHBOARD
        // ===============================
        public IActionResult Dashboard()
        {
            var songs = _context.Songs;
            return View("~/Views/Admin backup/Dashboard.cshtml", songs);
        }

        // ===============================
        // ADD SONG (GET)
        // ===============================
        public IActionResult AddSong()
        {
            return View("~/Views/Admin backup/AddSong.cshtml");
        }

        // ===============================
        // ADD SONG (POST)
        // ===============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSong(
            Song song,
            IFormFile songFile,
            IFormFile coverImage)
        {
            // Create folders if missing
            string musicFolder = Path.Combine(_environment.WebRootPath, "music");
            string imagesFolder = Path.Combine(_environment.WebRootPath, "images");

            Directory.CreateDirectory(musicFolder);
            Directory.CreateDirectory(imagesFolder);

            // ===============================
            // SAVE SONG FILE
            // ===============================
            if (songFile != null && songFile.Length > 0)
            {
                string songFileName = Guid.NewGuid() + Path.GetExtension(songFile.FileName);
                string songPath = Path.Combine(musicFolder, songFileName);

                using (var stream = new FileStream(songPath, FileMode.Create))
                {
                    await songFile.CopyToAsync(stream);
                }

                song.FilePath = "/music/" + songFileName;
            }

            // ===============================
            // SAVE COVER IMAGE
            // ===============================
            if (coverImage != null && coverImage.Length > 0)
            {
                string coverFileName = Guid.NewGuid() + Path.GetExtension(coverImage.FileName);
                string coverPath = Path.Combine(imagesFolder, coverFileName);

                using (var stream = new FileStream(coverPath, FileMode.Create))
                {
                    await coverImage.CopyToAsync(stream);
                }

                song.CoverImage = "/images/" + coverFileName;
            }

            // ===============================
            // SAVE TO DATABASE
            // ===============================
            _context.Songs.Add(song);
            await _context.SaveChangesAsync();

            return RedirectToAction("Dashboard");
        }

        // ===============================
        // DELETE SONG
        // ===============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSong(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song == null)
                return NotFound();

            // Delete song file
            if (!string.IsNullOrEmpty(song.FilePath))
            {
                var songPath = Path.Combine(
                    _environment.WebRootPath,
                    song.FilePath.TrimStart('/'));

                if (System.IO.File.Exists(songPath))
                    System.IO.File.Delete(songPath);
            }

            // Delete cover image
            if (!string.IsNullOrEmpty(song.CoverImage))
            {
                var coverPath = Path.Combine(
                    _environment.WebRootPath,
                    song.CoverImage.TrimStart('/'));

                if (System.IO.File.Exists(coverPath))
                    System.IO.File.Delete(coverPath);
            }

            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();

            return RedirectToAction("Dashboard");
        }
    }
}
