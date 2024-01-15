using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RolesAuth.Data;
using RolesAuth.Models;

namespace RolesAuth.Controllers
{
    public class CafeEntitiesController : Controller
    {
        private readonly AppDbContext _context;

        public CafeEntitiesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CafeEntities
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Cafes.Include(c => c.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: CafeEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cafes == null)
            {
                return NotFound();
            }

            var cafeEntity = await _context.Cafes
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CafeId == id);
            if (cafeEntity == null)
            {
                return NotFound();
            }

            return View(cafeEntity);
        }

        // GET: CafeEntities/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            return View();
        }

        // POST: CafeEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CafeId,Name,Email,Address,UserId")] CafeEntity cafeEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cafeEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", cafeEntity.UserId);
            return View(cafeEntity);
        }

        // GET: CafeEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cafes == null)
            {
                return NotFound();
            }

            var cafeEntity = await _context.Cafes.FindAsync(id);
            if (cafeEntity == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", cafeEntity.UserId);
            return View(cafeEntity);
        }

        // POST: CafeEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CafeId,Name,Email,Address,UserId")] CafeEntity cafeEntity)
        {
            if (id != cafeEntity.CafeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cafeEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CafeEntityExists(cafeEntity.CafeId))
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
            ViewData["UserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", cafeEntity.UserId);
            return View(cafeEntity);
        }

        // GET: CafeEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cafes == null)
            {
                return NotFound();
            }

            var cafeEntity = await _context.Cafes
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CafeId == id);
            if (cafeEntity == null)
            {
                return NotFound();
            }

            return View(cafeEntity);
        }

        // POST: CafeEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cafes == null)
            {
                return Problem("Entity set 'AppDbContext.Cafes'  is null.");
            }
            var cafeEntity = await _context.Cafes.FindAsync(id);
            if (cafeEntity != null)
            {
                _context.Cafes.Remove(cafeEntity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CafeEntityExists(int id)
        {
          return (_context.Cafes?.Any(e => e.CafeId == id)).GetValueOrDefault();
        }
    }
}
