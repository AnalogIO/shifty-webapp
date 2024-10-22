using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt;
using Shifty.App.DomainModels;
using Shifty.App.Repositories;

namespace Shifty.App.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseService(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        public async Task<Try<IEnumerable<Purchase>>> GetPurchases(int userId)
        {
            return await _purchaseRepository
                            .GetPurchases(userId)
                            .Map(x => x.Map(p => Purchase.FromDto(p)));
        }

        public async Task<Try<Purchase>> RefundPurchase(int purchaseId)
        {
            return await _purchaseRepository
                            .RefundPurchase(purchaseId)
                            .Map(x => x.Map(p => Purchase.FromDto(p)));
        }
    }
}