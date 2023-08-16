using Volo.Abp.Application.Dtos;
using System;

namespace ProductManagement.Carts
{
    public class GetCartsInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public Guid? UserId { get; set; }
        public int? QuantityMin { get; set; }
        public int? QuantityMax { get; set; }
        public DateTime? DateAddedMin { get; set; }
        public DateTime? DateAddedMax { get; set; }
        public decimal? UnitPriceMin { get; set; }
        public decimal? UnitPriceMax { get; set; }
        public decimal? TotalPriceMin { get; set; }
        public decimal? TotalPriceMax { get; set; }
        public DateTime? LastModifiedMin { get; set; }
        public DateTime? LastModifiedMax { get; set; }
        public Guid? BookId { get; set; }

        public GetCartsInput()
        {

        }
    }
}