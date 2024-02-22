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
    public class BillEntitiesController : Controller
    {
        private readonly AppDbContext _context;

        public BillEntitiesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: BillEntities
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Bill.Include(b => b.Order);
            return View(await appDbContext.ToListAsync());
        }

        // GET: BillEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bill == null)
            {
                return NotFound();
            }

            var billEntity = await _context.Bill
                .Include(b => b.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (billEntity == null)
            {
                return NotFound();
            }

            return View(billEntity);
        }

        // GET: BillEntities/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Order, "OrderId", "OrderId");
            return View();
        }

        // POST: BillEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TotalAmount,OrderId")] BillEntity billEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(billEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Order, "OrderId", "OrderId", billEntity.OrderId);
            return View(billEntity);
        }

        // GET: BillEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bill == null)
            {
                return NotFound();
            }

            var billEntity = await _context.Bill.FindAsync(id);
            if (billEntity == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Order, "OrderId", "OrderId", billEntity.OrderId);
            return View(billEntity);
        }

        // POST: BillEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TotalAmount,OrderId")] BillEntity billEntity)
        {
            if (id != billEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(billEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillEntityExists(billEntity.Id))
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
            ViewData["OrderId"] = new SelectList(_context.Order, "OrderId", "OrderId", billEntity.OrderId);
            return View(billEntity);
        }

        // GET: BillEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bill == null)
            {
                return NotFound();
            }

            var billEntity = await _context.Bill
                .Include(b => b.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (billEntity == null)
            {
                return NotFound();
            }

            return View(billEntity);
        }

        // POST: BillEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bill == null)
            {
                return Problem("Entity set 'AppDbContext.Bill'  is null.");
            }
            var billEntity = await _context.Bill.FindAsync(id);
            if (billEntity != null)
            {
                _context.Bill.Remove(billEntity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillEntityExists(int id)
        {
          return (_context.Bill?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
