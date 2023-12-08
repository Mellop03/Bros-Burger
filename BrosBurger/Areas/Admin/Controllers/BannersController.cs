using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BrosBurger.Context;
using BrosBurger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BrosBurger.Context;
using App.Filters;

namespace BrosBurger.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class BannersController : Controller
    {
        private readonly AppDbContext _context;

        public BannersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Banners
        public async Task<IActionResult> Index()
        {
            return _context.Banners != null ?
                        View(await _context.Banners.ToListAsync()) :
                        Problem("Entity set 'AppDbContext.Banners'  is null.");
        }

        // GET: Banners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Banners == null)
            {
                return NotFound();
            }

            var banners = await _context.Banners
                .FirstOrDefaultAsync(m => m.BannerId == id);
            if (banners == null)
            {
                return NotFound();
            }

            return View(banners);
        }

        // GET: Banners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Banners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BannerId,NomeBanner,ImagemBanner")] Banners banners)
        {
            if (true)
            {
                _context.Add(banners);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(banners);
        }

        // GET: Banners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Banners == null)
            {
                return NotFound();
            }

            var banners = await _context.Banners.FindAsync(id);
            if (banners == null)
            {
                return NotFound();
            }
            ViewData["BannerId"] = new SelectList(_context.Banners, "BannerID", "BannerID", banners.BannerId);
            return View(banners);
        }

        // POST: Banners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BannerId,NomeBanner,ImagemBanner")] Banners banners)
        {
            if (id != banners.BannerId)
            {
                return NotFound();
            }

            if (true)
            {
                try
                {
                    _context.Update(banners);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BannersExists(banners.BannerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(banners);
        }

        // GET: Banners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Banners == null)
            {
                return NotFound();
            }

            var banners = await _context.Banners
                .FirstOrDefaultAsync(m => m.BannerId == id);
            if (banners == null)
            {
                return NotFound();
            }

            return View(banners);
        }

        // POST: Banners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Banners == null)
            {
                return Problem("Entity set 'AppDbContext.Banners'  is null.");
            }
            var banners = await _context.Banners.FindAsync(id);
            if (banners != null)
            {
                _context.Banners.Remove(banners);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BannersExists(int id)
        {
            return (_context.Banners?.Any(e => e.BannerId == id)).GetValueOrDefault();
        }
    }
}
