using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.Common;
using Shifty.Api.Generated.AnalogCoreV2;
using Shifty.App.DomainModels;

namespace Shifty.App.Services
{
    public interface IPurchaseService
    {
        Task<Try<IEnumerable<Purchase>>> GetPurchases(int userId);
        Task<Try<Purchase>> RefundPurchase(int purchaseId);
    }
}