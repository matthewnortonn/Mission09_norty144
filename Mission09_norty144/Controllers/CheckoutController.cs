using Microsoft.AspNetCore.Mvc;
using Mission09_norty144.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission09_norty144.Controllers
{
    public class CheckoutController : Controller
    {
        private ICheckoutRepository repo { get; set; }
        private Cart cart { get; set; }

        //checkout controller used with the variables of repo, cart and instance of c
        public CheckoutController (ICheckoutRepository temp, Cart c)
        {
            repo = temp;
            cart = c;
        }

        //using HttpGet to get the checkout information
        [HttpGet]
        public IActionResult Checkout()
        {
            return View(new Checkout());
        }

        //HttpPost for when the guest submits their cart, if the cart is already empty then they get an alert.
        [HttpPost]
        public IActionResult Checkout(Checkout checkout)
        {
            if (cart.Items.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty.");
            }

            //model state validation and page redirection
            if (ModelState.IsValid)
            {
                checkout.Lines = cart.Items.ToArray();
                repo.SaveCheckout(checkout);
                cart.ClearCart();

                return RedirectToPage("/CheckoutCompleted");
            }
            else
            {
                return View();
            }
        }
    }
}
