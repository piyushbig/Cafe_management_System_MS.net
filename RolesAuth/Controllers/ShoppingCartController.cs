using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RolesAuth.Data;
using RolesAuth.Models;
using System.Security.Claims;

namespace RolesAuth.Controllers
{
    public class ShoppingCartController : Controller

        
    {

        private readonly AppDbContext dbContext;

        public ShoppingCartController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;

        }
        // GET: ShoppingCartController1
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUser = await dbContext.CustomerEntity
                .FirstOrDefaultAsync(c => c.UserId == currentUserId);

            var product = await dbContext.Products.FindAsync(productId);

            if (product != null)
            {
                // Example: Add the product to the cartitems table
                var cartItem = new CartItems
                {
                    ProductId = product.ProductId,
                    CustomerId = currentUser.CustomerId,
                    CartFood_name = product.Name,
                    Cafe_name = "cafe",
                    Category = "drink",
                    Price = product.Prize,
                    Quantity = 1,
                    // other properties...
                };

                dbContext.CartItems.Add(cartItem);
                dbContext.SaveChanges();

                // Redirect to the home page after adding to the cart
                return RedirectToAction("Index", "CartItems");
            }

            // Handle the case where the product is not found (optional)
            return RedirectToAction("ProductNotFound", "Error"); // You can customize this to your needs
        }

        // GET: ShoppingCartController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ShoppingCartController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShoppingCartController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ShoppingCartController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ShoppingCartController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ShoppingCartController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ShoppingCartController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
