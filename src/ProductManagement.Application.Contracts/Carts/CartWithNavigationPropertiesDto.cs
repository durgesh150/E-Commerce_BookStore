using ProductManagement.Books;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace ProductManagement.Carts
{
    public class CartWithNavigationPropertiesDto
    {
        public CartDto Cart { get; set; }

        public BookDto Book { get; set; }

    }
}