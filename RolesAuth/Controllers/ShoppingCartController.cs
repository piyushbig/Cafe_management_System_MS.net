using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RolesAuth.Data;
using RolesAuth.Models;
using Stripe.Checkout;
using System.Security.Claims;

namespace RolesAuth.Controllers
{

    [Authorize]
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

        public IActionResult OrderConfirmation()
        {
            var service = new SessionService();
            Session session = service.Get(TempData["Session"].ToString());

            if (session.PaymentStatus == "paid")
            {
                var transaction = session.PaymentIntentId.ToString();
                return View("success");
            }

            return View("login");
        }

        public IActionResult success()
        {
            return View();
        }

        public IActionResult login()
        {
            return View();
        }

        public IActionResult CheckOut()
        {
            List<CartItems> cartItems = dbContext.CartItems?.ToList() ?? new List<CartItems>();

            var domain = "https://localhost:7211/";

            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + $"ShoppingCart/OrderConfirmation",
                CancelUrl = domain + "ShoppingCart/Login",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment"
            };


            foreach (var item in cartItems)
            {
                var sessionListItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Price * item.Quantity*100),
                        Currency = "inr",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.CartFood_name?.ToString() ?? "Product Name Not Available", // Handle if Product is null
                        }
                    },
                    Quantity = 1 // item.Quantity
                };

                options.LineItems.Add(sessionListItem);
            }

            var service = new SessionService();
            Session session = service.Create(options);

            TempData["Session"] = session.Id;

            Response.Headers.Add("Location", session.Url);

            return new StatusCodeResult(303);
        }
    }

    }
