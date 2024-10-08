using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.Common;
using Shifty.Api.Generated.AnalogCoreV1;
using Shifty.Api.Generated.AnalogCoreV2;
using Shifty.App.Services;
using static LanguageExt.Prelude;

namespace Shifty.App.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly AnalogCoreV2 _client;

        public PurchaseRepository(AnalogCoreV2 client)
        {
            _client = client;
        }

        async Task<Try<IEnumerable<SimplePurchaseResponse>>> IPurchaseRepository.GetPurchases(int userId)
        {
            return await TryAsync(async () => (await _client.ApiV2PurchasesUserAsync(userId)).AsEnumerable());
        }

        async Task<Try<SimplePurchaseResponse>> IPurchaseRepository.RefundPurchase(int purchaseId)
        {
            return await TryAsync(async () => await _client.ApiV2PurchasesRefundAsync(purchaseId));
        }
    }
}