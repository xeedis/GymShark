using Braintree;
using GymSharkApi.Entities;
using GymSharkApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymSharkApi.Controllers
{
    public class PaymentController:BaseApiController
    {
        private readonly IBraintreeService _braintreeService;

        public PaymentController(IBraintreeService braintreeService)
        {
            _braintreeService = braintreeService;
        }

        [HttpPost]
        public IActionResult Create([FromQuery]ProductPurharseVM model)
        {
            var gateway = _braintreeService.GetGateway();
            var request = new TransactionRequest
            {
                Amount = Convert.ToDecimal("250"),
                PaymentMethodNonce = model.Nonce,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };

            var result = gateway.Transaction.Sale(request);

            return Ok(result); 
        }
    }
}
