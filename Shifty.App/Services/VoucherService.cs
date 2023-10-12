using System.Collections.Generic;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using LanguageExt.UnsafeValueAccess;
using Shifty.Api.Generated.AnalogCoreV1;
using Shifty.Api.Generated.AnalogCoreV2;
using Shifty.App.Authentication;
using Shifty.App.Repositories;

namespace Shifty.App.Services
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherRepository _voucherRepository;
        
        public VoucherService(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }

        public async Task<(bool, ICollection<IssueVoucherResponse>)> IssueVouchers(int amount, int productId, string description)
        {
            var either = await _voucherRepository.IssueAsync(amount: amount, productId: productId, description: description);

            return either.IsLeft ? (false, null) : (true, either.ValueUnsafe());
        }
    }
}