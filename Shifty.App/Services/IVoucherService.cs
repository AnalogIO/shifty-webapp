using System.Collections.Generic;
using System.Threading.Tasks;
using Shifty.Api.Generated.AnalogCoreV2;

namespace Shifty.App.Services
{
    public interface IVoucherService
    {
        Task<ICollection<IssueVoucherResponse>> IssueVouchers(int amount, int productId, string description);
    }
}