using ProductManagement.Books;

using System;
using System.Collections.Generic;

namespace ProductManagement.Carts
{
    public class CartWithNavigationProperties
    {
        public Cart Cart { get; set; }

        public Book Book { get; set; }
        

        
    }
}