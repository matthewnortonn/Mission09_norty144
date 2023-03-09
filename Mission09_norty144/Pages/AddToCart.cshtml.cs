using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mission09_norty144.Infrastructure;
using Mission09_norty144.Models;

namespace Mission09_norty144.Pages
{
    public class AddToCartModel : PageModel
    {
        private IBookstoreRepository repo { get; set; }

        //temporary database for the data in the cart, this is because we don't have a controller here
        public AddToCartModel (IBookstoreRepository temp)
        {
            repo = temp;
        }


        public Cart cart { get; set; }
        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl)
        {

            ReturnUrl = returnUrl ?? "/";
            cart = HttpContext.Session.GetJSon<Cart>("cart") ?? new Cart();
        }

        public IActionResult OnPost(int BookId, string returnUrl)
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookId == BookId);

            cart = HttpContext.Session.GetJSon<Cart>("cart") ?? new Cart();

            cart.AddItem(b, 1);

            HttpContext.Session.SetJson("cart", cart);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}
