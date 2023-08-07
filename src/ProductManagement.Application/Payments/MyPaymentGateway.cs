//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Volo.Abp.DependencyInjection;
//using Volo.Payment.Gateways;
//using Volo.Payment.Requests;

//namespace ProductManagement.Payments
//{
//    public class MyPaymentGateway : IPaymentGateway, ITransientDependency
//    {
//        private readonly IPaymentRequestRepository paymentRequestRepository;

//        public MyPaymentGateway(IPaymentRequestRepository paymentRequestRepository)
//        {
//            this.paymentRequestRepository = paymentRequestRepository;
//        }

//        public async Task<PaymentRequestStartResult> StartAsync(PaymentRequest paymentRequest, PaymentRequestStartInput input)
//        {
//            var totalPrice = paymentRequest.Products.Sum(x => x.TotalPrice);

//            //var checkoutLink = // Some operations here

//          return new PaymentRequestStartResult
//          {
//              CheckoutLink = checkoutLink + "returnUrl=" + input.ReturnUrl
//          };
//        }

//        public async Task<PaymentRequest> CompleteAsync(Dictionary<string, string> parameters)
//        {
//            var token = parameters["token"]; // You can get any parameter from your gateway provides. Example: token, id, hash etc. 

//            var result = ;// provider.Validate(token); Validate the payment here

//            var paymentRequest = await paymentRequestRepository.FindAsync(result.Id);

//            paymentRequest.SetState(PaymentRequestState.Completed); // completed or anything else according to your result.

//            return paymentRequest;
//        }

//        public Task HandleWebhookAsync(string payload, Dictionary<string, string> headers)
//        {
//            // You can leave unimplemented if you not configure webhooks.
//            throw new System.NotImplementedException();
//        }

//        public bool IsValid(PaymentRequest paymentRequest, Dictionary<string, string> properties)
//        {
//            // You can check some custom logic here to make this gateway available or not.
//            return true;
//        }
//    }
//}
