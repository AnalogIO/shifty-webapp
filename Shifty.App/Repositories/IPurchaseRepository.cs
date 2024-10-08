using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.Common;
using Shifty.Api.Generated.AnalogCoreV1;
using Shifty.Api.Generated.AnalogCoreV2;

namespace Shifty.App.Repositories
{
    public interface IPurchaseRepository
    {
        public Task<Try<System.Collections.Generic.IEnumerable<SimplePurchaseResponse>>> GetPurchases(int userId);
        public Task<Try<SimplePurchaseResponse>> RefundPurchase(int purchaseId);
    }
}