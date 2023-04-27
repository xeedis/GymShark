using Braintree;
using GymSharkApi.Helpers;
using GymSharkApi.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymSharkApi.Services
{
    public class BraintreeService : IBraintreeService
    {
        private readonly IOptions<BraintreeSettings> _config;

        public BraintreeService(IOptions<BraintreeSettings> config)
        {
            _config = config;
        }

        public IBraintreeGateway CreateGateway()
        {
            var newGateway = new BraintreeGateway()
            {
                Environment = Braintree.Environment.SANDBOX,
                MerchantId = _config.Value.MerchantId,
                PublicKey = _config.Value.PublicKey,
                PrivateKey = _config.Value.PrivateKey
            };

            return newGateway;
        }

        public IBraintreeGateway GetGateway()
        {
            return CreateGateway();
        }
    }
}
