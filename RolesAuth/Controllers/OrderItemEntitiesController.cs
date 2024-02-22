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
    public class OrderItemEntitiesController : Controller
    {
        private readonly AppDbContext _context;

        public OrderItemEntitiesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: OrderItemEntities
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.OrderItem.Include(o => o.Order).Include(o => o.Product);
            return View(await appDbContext.ToListAsync());
        }

        // GET: OrderItemEntities/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.OrderItem == null)
            {
                return NotFound();
            }

            var orderItemEntity = await _context.OrderItem
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderItemId == id);
            if (orderItemEntity == null)
            {
                return NotFound();
            }

            return View(orderItemEntity);
        }

        // GET: OrderItemEntities/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Order, "OrderId", "OrderId");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            return View();
        }

        // POST: OrderItemEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderItemId,OrderId,ProductId,Quantity,Subtotal")] OrderItemEntity orderItemEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderItemEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Order, "OrderId", "OrderId", orderItemEntity.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", orderItemEntity.ProductId);
            return View(orderItemEntity);
        }

        // GET: OrderItemEntities/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.OrderItem == null)
            {
                return NotFound();
            }

            var orderItemEntity = await _context.OrderItem.FindAsync(id);
            if (orderItemEntity == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Order, "OrderId", "OrderId", orderItemEntity.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", orderItemEntity.ProductId);
            return View(orderItemEntity);
        }

        // POST: OrderItemEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("OrderItemId,OrderId,ProductId,Quantity,Subtotal")] OrderItemEntity orderItemEntity)
        {
            if (id != orderItemEntity.OrderItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderItemEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderItemEntityExists(orderItemEntity.OrderItemId))
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
            ViewData["OrderId"] = new SelectList(_context.Order, "OrderId", "OrderId", orderItemEntity.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", orderItemEntity.ProductId);
            return View(orderItemEntity);
        }

        // GET: OrderItemEntities/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.OrderItem == null)
            {
                return NotFound();
            }

            var orderItemEntity = await _context.OrderItem
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderItemId == id);
            if (orderItemEntity == null)
            {
                return NotFound();
            }

            return View(orderItemEntity);
        }

        // POST: OrderItemEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.OrderItem == null)
            {
                return Problem("Entity set 'AppDbContext.OrderItem'  is null.");
            }
            var orderItemEntity = await _context.OrderItem.FindAsync(id);
            if (orderItemEntity != null)
            {
                _context.OrderItem.Remove(orderItemEntity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderItemEntityExists(long id)
        {
          return (_context.OrderItem?.Any(e => e.OrderItemId == id)).GetValueOrDefault();
        }
    }
}
