using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RolesAuth.Data;
using RolesAuth.Models;

namespace RolesAuth.Controllers
{
    [Authorize (Roles="Cafe")]
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
              return _context.Cafes != null ? 
                          View(await _context.Cafes.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Cafes'  is null.");
        }

        // GET: CafeEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cafes == null)
            {
                return NotFound();
            }

            var cafeEntity = await _context.Cafes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cafeEntity == null)
            {
                return NotFound();
            }

            return View(cafeEntity);
        }

        // GET: CafeEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CafeEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Password,Address")] CafeEntity cafeEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cafeEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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
            return View(cafeEntity);
        }

        // POST: CafeEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Password,Address")] CafeEntity cafeEntity)
        {
            if (id != cafeEntity.Id)
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
                    if (!CafeEntityExists(cafeEntity.Id))
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
                .FirstOrDefaultAsync(m => m.Id == id);
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
          return (_context.Cafes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
