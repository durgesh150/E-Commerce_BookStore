﻿using System;

namespace ProductManagement.Web.ViewModels
{
    public class BookViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public float Price { get; set; }
        public int CartQuantity { get; set; }
    }
}
