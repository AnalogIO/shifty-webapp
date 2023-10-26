using System.Collections.Generic;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using LanguageExt;
using LanguageExt.Common;
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

        public async Task<Try<ICollection<IssueVoucherResponse>>> IssueVouchers(int amount, int productId, string description, string requester, string prefix)
        {
            return await _voucherRepository.IssueAsync(amount: amount, productId: productId, requester: requester, description: description, prefix: prefix);
        }
    }
}