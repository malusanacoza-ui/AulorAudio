using AulorAudio.Data;
using AulorAudio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace AulorAudio.Controllers
{
    [Authorize]
    public class SongActionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public SongActionsController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Like(string file)
        {
            var userId = _userManager.GetUserId(User);
            file = Path.GetFileName(file);

            if (!_context.SongLikes.Any(l => l.SongFile == file && l.UserId == userId))
            {
                _context.SongLikes.Add(new SongLike
                {
                    SongFile = file,
                    UserId = userId
                });

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Songs");
        }

        public async Task<IActionResult> Favorite(string file)
        {
            var userId = _userManager.GetUserId(User);
            file = Path.GetFileName(file);

            if (!_context.FavoriteSongs.Any(f => f.SongFile == file && f.UserId == userId))
            {
                _context.FavoriteSongs.Add(new FavoriteSong
                {
                    SongFile = file,
                    UserId = userId
                });

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Songs");
        }
        public async Task<IActionResult> MyFavorites()
        {
            var userId = _userManager.GetUserId(User);

            var files = await _context.FavoriteSongs
                .Where(f => f.UserId == userId)
                .Select(f => f.SongFile)
                .ToListAsync();

            return View(files);
        }
        public bool IsLiked(string file, string userId)
        {
            return _context.SongLikes.Any(l => l.SongFile == file && l.UserId == userId);
        }
        [Authorize]
        public IActionResult LikedFiles()
        {
            var userId = _userManager.GetUserId(User);

            var liked = _context.SongLikes
                .Where(l => l.UserId == userId)
                .Select(l => l.SongFile)
                .ToList();

            return Json(liked);
        }

    }
}
