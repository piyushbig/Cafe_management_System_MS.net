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
    public class CategoriesEntitiesController : Controller
    {
        private readonly AppDbContext _context;

        public CategoriesEntitiesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CategoriesEntities
        public async Task<IActionResult> Index()
        {
              return _context.Categories != null ? 
                          View(await _context.Categories.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Categories'  is null.");
        }

        // GET: CategoriesEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var categoriesEntity = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (categoriesEntity == null)
            {
                return NotFound();
            }

            return View(categoriesEntity);
        }

        // GET: CategoriesEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoriesEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,Name,Description")] CategoriesEntity categoriesEntity)
        {
            //if (ModelState.IsValid)
            {
                _context.Add(categoriesEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoriesEntity);
        }

        // GET: CategoriesEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var categoriesEntity = await _context.Categories.FindAsync(id);
            if (categoriesEntity == null)
            {
                return NotFound();
            }
            return View(categoriesEntity);
        }

        // POST: CategoriesEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,Name,Description")] CategoriesEntity categoriesEntity)
        {
            if (id != categoriesEntity.CategoryId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoriesEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriesEntityExists(categoriesEntity.CategoryId))
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
            return View(categoriesEntity);
        }

        // GET: CategoriesEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var categoriesEntity = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (categoriesEntity == null)
            {
                return NotFound();
            }

            return View(categoriesEntity);
        }

        // POST: CategoriesEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'AppDbContext.Categories'  is null.");
            }
            var categoriesEntity = await _context.Categories.FindAsync(id);
            if (categoriesEntity != null)
            {
                _context.Categories.Remove(categoriesEntity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriesEntityExists(int id)
        {
          return (_context.Categories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
