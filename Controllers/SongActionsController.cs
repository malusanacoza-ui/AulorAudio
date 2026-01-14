using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AulorAudio.Data;
using AulorAudio.Models;

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
    }
}
