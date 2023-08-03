using ProductManagement.Authors;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace ProductManagement.Books
{
    public class BookWithNavigationPropertiesDto
    {
        public BookDto Book { get; set; }

        public AuthorDto Author { get; set; }

    }
}