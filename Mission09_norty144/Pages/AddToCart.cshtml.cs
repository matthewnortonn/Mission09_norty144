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
        //establishing the variables
        private IBookstoreRepository repo { get; set; }
        public Cart cart { get; set; }
        public string ReturnUrl { get; set; }


        //temporary database for the data in the cart, this is because we don't have a controller here
        public AddToCartModel (IBookstoreRepository temp, Cart c)
        {
            repo = temp;
            cart = c;
        }


        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }

        //Here is the code for the OnPost method from the checkout
        public IActionResult OnPost(int BookId, string returnUrl)
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookId == BookId);

            cart.AddItem(b, 1);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }


        //here is the method for removing a book from the cart if the customer would like to
        public IActionResult OnPostRemove(int BookId, string returnUrl)
        {
            cart.RemoveBook(cart.Items.First(x => x.Book.BookId == BookId).Book);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}