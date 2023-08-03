using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ProductManagement.Carts
{
    public class CartCreateDto
    {
        public Guid UserId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateAdded { get; set; }
        public Guid? BookId { get; set; }
    }
}