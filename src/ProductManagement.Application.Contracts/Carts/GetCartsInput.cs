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
        public Guid? BookId { get; set; }

        public GetCartsInput()
        {

        }
    }
}