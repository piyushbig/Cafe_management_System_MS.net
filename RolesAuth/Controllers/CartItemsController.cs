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
    public class CartItemsController : Controller
    {
        private readonly AppDbContext _context;

        public CartItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CartItems
        public async Task<IActionResult> Index()
        {
              return _context.CartItems != null ? 
                          View(await _context.CartItems.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.CartItems'  is null.");
        }

        // GET: CartItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CartItems == null)
            {
                return NotFound();
            }

            var cartItems = await _context.CartItems
                .FirstOrDefaultAsync(m => m.CartFood_id == id);
            if (cartItems == null)
            {
                return NotFound();
            }

            return View(cartItems);
        }

        // GET: CartItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CartItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartFood_id,CartFood_name,Cafe_name,Category,Price,Quantity")] CartItems cartItems)
        {
            /*if (ModelState.IsValid)
            {*/
                _context.Add(cartItems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            /*}*/
            return View(cartItems);
        }

        // GET: CartItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CartItems == null)
            {
                return NotFound();
            }

            var cartItems = await _context.CartItems.FindAsync(id);
            if (cartItems == null)
            {
                return NotFound();
            }
            return View(cartItems);
        }

        // POST: CartItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CartFood_id,CartFood_name,Cafe_name,Category,Price,Quantity")] CartItems cartItems)
        {
            if (id != cartItems.CartFood_id)
            {
                return NotFound();
            }

            /*if (ModelState.IsValid)
            {*/
                try
                {
                    _context.Update(cartItems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartItemsExists(cartItems.CartFood_id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            /*}*/
            return View(cartItems);
        }

        // GET: CartItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CartItems == null)
            {
                return NotFound();
            }

            var cartItems = await _context.CartItems
                .FirstOrDefaultAsync(m => m.CartFood_id == id);
            if (cartItems == null)
            {
                return NotFound();
            }

            return View(cartItems);
        }

        // POST: CartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CartItems == null)
            {
                return Problem("Entity set 'AppDbContext.CartItems'  is null.");
            }
            var cartItems = await _context.CartItems.FindAsync(id);
            if (cartItems != null)
            {
                _context.CartItems.Remove(cartItems);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartItemsExists(int id)
        {
          return (_context.CartItems?.Any(e => e.CartFood_id == id)).GetValueOrDefault();
        }
    }
}
