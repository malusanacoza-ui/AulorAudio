using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AulorAudio.Data;

namespace AulorAudio.Controllers
{
    public class SongsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SongsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Songs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Songs.ToListAsync());
        }

        // GET: Songs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // GET: Songs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Artist,FilePath,CoverImage,UploadDate")] Song song)
        {
            if (ModelState.IsValid)
            {
                _context.Add(song);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(song);
        }

        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Song song, IFormFile mp3File, IFormFile coverImage)
        {
            if (!ModelState.IsValid)
                return View(song);

            // Save MP3
            if (mp3File != null)
            {
                string musicPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/music");
                Directory.CreateDirectory(musicPath);

                string fileName = Guid.NewGuid() + Path.GetExtension(mp3File.FileName);
                string fullPath = Path.Combine(musicPath, fileName);

                using var stream = new FileStream(fullPath, FileMode.Create);
                await mp3File.CopyToAsync(stream);

                song.FilePath = "/music/" + fileName;
            }

            // Save Cover Image
            if (coverImage != null)
            {
                string coverPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/covers");
                Directory.CreateDirectory(coverPath);

                string imgName = Guid.NewGuid() + Path.GetExtension(coverImage.FileName);
                string imgFullPath = Path.Combine(coverPath, imgName);

                using var stream = new FileStream(imgFullPath, FileMode.Create);
                await coverImage.CopyToAsync(stream);

                song.CoverImage = "/covers/" + imgName;
            }

            song.UploadDate = DateTime.Now;

            _context.Add(song);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song != null)
            {
                _context.Songs.Remove(song);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongExists(int id)
        {
            return _context.Songs.Any(e => e.Id == id);
        }
    }
}
