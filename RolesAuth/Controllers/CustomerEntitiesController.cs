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
    public class CustomerEntitiesController : Controller
    {
        private readonly AppDbContext _context;

        public CustomerEntitiesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CustomerEntities
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.CustomerEntity.Include(c => c.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: CustomerEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CustomerEntity == null)
            {
                return NotFound();
            }

            var customerEntity = await _context.CustomerEntity
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerEntity == null)
            {
                return NotFound();
            }

            return View(customerEntity);
        }

        // GET: CustomerEntities/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            return View();
        }

        // POST: CustomerEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Address,UserId")] CustomerEntity customerEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", customerEntity.UserId);
            return View(customerEntity);
        }

        // GET: CustomerEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CustomerEntity == null)
            {
                return NotFound();
            }

            var customerEntity = await _context.CustomerEntity.FindAsync(id);
            if (customerEntity == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", customerEntity.UserId);
            return View(customerEntity);
        }

        // POST: CustomerEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Address,UserId")] CustomerEntity customerEntity)
        {
            if (id != customerEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerEntityExists(customerEntity.Id))
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
            ViewData["UserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", customerEntity.UserId);
            return View(customerEntity);
        }

        // GET: CustomerEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CustomerEntity == null)
            {
                return NotFound();
            }

            var customerEntity = await _context.CustomerEntity
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerEntity == null)
            {
                return NotFound();
            }

            return View(customerEntity);
        }

        // POST: CustomerEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CustomerEntity == null)
            {
                return Problem("Entity set 'AppDbContext.CustomerEntity'  is null.");
            }
            var customerEntity = await _context.CustomerEntity.FindAsync(id);
            if (customerEntity != null)
            {
                _context.CustomerEntity.Remove(customerEntity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerEntityExists(int id)
        {
          return (_context.CustomerEntity?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
