using Microsoft.AspNetCore.Mvc;
using ProductManagement.Controllers;
using System.Threading.Tasks;
using Volo.Payment.Gateways;
using Volo.Payment.Requests;

namespace ProductManagement.Web.Payments
{
    public class PaymentController : ProductManagementController
    {
        private readonly IPaymentGateway _paymentGateway;
        public PaymentController(IPaymentGateway paymentGateway)
        {
            _paymentGateway = paymentGateway;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentRequestDto paymentRequestDto)
        {
            // Convert the DTO to a PaymentRequest object
            var paymentRequest = ObjectMapper.Map<PaymentRequestDto, PaymentRequest>(paymentRequestDto);

            // Create the payment
           

            // Return the result
            return Ok();
        }

    }
}
