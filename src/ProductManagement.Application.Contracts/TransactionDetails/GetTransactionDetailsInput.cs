using Volo.Abp.Application.Dtos;
using System;

namespace ProductManagement.TransactionDetails
{
    public class GetTransactionDetailsInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public int? QuantityMin { get; set; }
        public int? QuantityMax { get; set; }
        public decimal? SubTotalMin { get; set; }
        public decimal? SubTotalMax { get; set; }
        public Guid? TransactionId { get; set; }
        public Guid? BookId { get; set; }

        public GetTransactionDetailsInput()
        {

        }
    }
}