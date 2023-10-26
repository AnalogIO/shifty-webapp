using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.Common;
using Shifty.Api.Generated.AnalogCoreV2;

namespace Shifty.App.Services
{
    public interface IVoucherService
    {
        Task<Try<ICollection<IssueVoucherResponse>>> IssueVouchers(int amount, int productId, string description, string requester, string prefix);
    }
}