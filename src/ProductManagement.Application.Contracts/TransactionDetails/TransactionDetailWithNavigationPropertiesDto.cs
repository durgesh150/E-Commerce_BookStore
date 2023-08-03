using ProductManagement.Transactions;
using ProductManagement.Books;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace ProductManagement.TransactionDetails
{
    public class TransactionDetailWithNavigationPropertiesDto
    {
        public TransactionDetailDto TransactionDetail { get; set; }

        public TransactionDto Transaction { get; set; }
        public BookDto Book { get; set; }

    }
}