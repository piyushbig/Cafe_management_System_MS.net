using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RolesAuth.Data;
using RolesAuth.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace RolesAuth.Controllers
{
    public class ProductEntitiesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductEntitiesController(AppDbContext context,IWebHostEnvironment webHost)
        {
            _context = context;
            webHostEnvironment = webHost;
        }

        // GET: ProductEntities
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Products.Include(p => p.Cafe).Include(p => p.Category);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ProductEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var productEntity = await _context.Products
                .Include(p => p.Cafe)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (productEntity == null)
            {
                return NotFound();
            }

            return View(productEntity);
        }
        public string UploadedFile(ProductEntity product)
        {
            string uniqueFileName = null;


            if (product.Image != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + product.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    product.Image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        // GET: ProductEntities/Create
        public IActionResult Create()
        {
            ViewData["CafeId"] = new SelectList(_context.Cafes, "CafeId", "CafeId");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            return View();
        }

        //POST: ProductEntities/Create
        //To protect from overposting attacks, enable the specific properties you want to bind to.
        //For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        //[HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Create([Bind("ProductId,Name,Prize,ImageUrl,CategoryId,CafeId")] ProductEntity productEntity)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         _context.Add(productEntity);
        //         await _context.SaveChangesAsync();
        //         return RedirectToAction(nameof(Index));
        //     }
        //     ViewData["CafeId"] = new SelectList(_context.Cafes, "CafeId", "CafeId", productEntity.CafeId);
        //     ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", productEntity.CategoryId);
        //     return View(productEntity);
        // }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductEntity product)
        {
            string uniqueFileName = UploadedFile(product);
            product.ImageUrl = uniqueFileName?.Trim();
            // Validate and sanitize the file path if needed


            _context.Attach(product);
            _context.Entry(product).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: ProductEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var productEntity = await _context.Products.FindAsync(id);
            if (productEntity == null)
            {
                return NotFound();
            }
            ViewData["CafeId"] = new SelectList(_context.Cafes, "CafeId", "CafeId", productEntity.CafeId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", productEntity.CategoryId);
            return View(productEntity);
        }

        // POST: ProductEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,Prize,ImageUrl,CategoryId,CafeId")] ProductEntity productEntity)
        {
            if (id != productEntity.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductEntityExists(productEntity.ProductId))
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
            ViewData["CafeId"] = new SelectList(_context.Cafes, "CafeId", "CafeId", productEntity.CafeId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", productEntity.CategoryId);
            return View(productEntity);
        }

        // GET: ProductEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var productEntity = await _context.Products
                .Include(p => p.Cafe)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (productEntity == null)
            {
                return NotFound();
            }

            return View(productEntity);
        }

        // POST: ProductEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'AppDbContext.Products'  is null.");
            }
            var productEntity = await _context.Products.FindAsync(id);
            if (productEntity != null)
            {
                _context.Products.Remove(productEntity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductEntityExists(int id)
        {
          return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
