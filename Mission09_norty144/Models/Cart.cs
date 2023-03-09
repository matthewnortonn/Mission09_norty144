﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission09_norty144.Models
{
    public class Cart
    {
        //this is for the cart and the ability to add books into the cart
        public List<CartLineItem> Items { get; set; } = new List<CartLineItem>();
        
        public void AddItem(Book booky, int qty)
        {
            CartLineItem line = Items
                .Where(b => b.Book.BookId == booky.BookId)
                .FirstOrDefault();

            //adding new books to the cart list
            if (line == null)
            {
                Items.Add(new CartLineItem
                {
                    Book = booky,
                    Quantity = qty
                });
            }
            else
            {
                line.Quantity += qty;
            }
                
        }

        public double CalculateTotal()
        {
            double sum = Items.Sum(x => x.Quantity * x.Book.Price);

            return sum;
        }

    }


    

    public class CartLineItem
    {
        public int LineID { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }

    }
}