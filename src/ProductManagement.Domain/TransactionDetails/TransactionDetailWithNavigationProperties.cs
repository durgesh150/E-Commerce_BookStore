using ProductManagement.Transactions;
using ProductManagement.Books;

using System;
using System.Collections.Generic;

namespace ProductManagement.TransactionDetails
{
    public class TransactionDetailWithNavigationProperties
    {
        public TransactionDetail TransactionDetail { get; set; }

        public Transaction Transaction { get; set; }
        public Book Book { get; set; }
        

        
    }
}