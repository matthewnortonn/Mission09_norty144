using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Mission09_norty144.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mission09_norty144.Models
{
    public class SessionCart : Cart
    {
        public static Cart GetCart (IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            //checking to see if a session already exists
            SessionCart cart = session?.GetJSon<SessionCart>("Cart") ?? new SessionCart();

            cart.Session = session;

            return cart;
        }

        //we add JsonIgnore to prevent an item from being serialized or de-serialized
        [JsonIgnore]
        public ISession Session { get; set; }

        //session override to add item
        public override void AddItem(Book booky, int qty)
        {
            base.AddItem(booky, qty);
            Session.SetJson("Cart", this);
        }

        //session override to remove a book from the current cart
        public override void RemoveBook(Book booky)
        {
            base.RemoveBook(booky);
            Session.SetJson("Cart", this);
        }

        //session override to clear the entire cart
        public override void ClearCart()
        {
            base.ClearCart();
            Session.Remove("Cart");
        }
    }
}
