using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ProductManagement.Web.Pages.Payments
{
    public class SucessModel : PageModel
    {
        public decimal TotalAmount { get; set; }
        public List<PurchasedItem> PurchasedItems { get; set; }
        public void OnGet(decimal totalAmount, List<PurchasedItem> purchasedItems)
        {
            TotalAmount = totalAmount;
            PurchasedItems = purchasedItems;
        }
    }
    public class PurchasedItem
    {
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
