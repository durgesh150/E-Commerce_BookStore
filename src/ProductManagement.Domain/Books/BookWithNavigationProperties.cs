using ProductManagement.Authors;

using System;
using System.Collections.Generic;

namespace ProductManagement.Books
{
    public class BookWithNavigationProperties
    {
        public Book Book { get; set; }

        public Author Author { get; set; }
        

        
    }
}