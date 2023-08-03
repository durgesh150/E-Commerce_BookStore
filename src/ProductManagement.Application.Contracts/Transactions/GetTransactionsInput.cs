using Volo.Abp.Application.Dtos;
using System;

namespace ProductManagement.Transactions
{
    public class GetTransactionsInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public Guid? UserId { get; set; }
        public DateTime? TransactionDateMin { get; set; }
        public DateTime? TransactionDateMax { get; set; }
        public decimal? TotalAmountMin { get; set; }
        public decimal? TotalAmountMax { get; set; }
        public string? PaymentStatus { get; set; }

        public GetTransactionsInput()
        {

        }
    }
}