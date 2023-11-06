using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.Common;
using Shifty.Api.Generated.AnalogCoreV2;
using static LanguageExt.Prelude;

namespace Shifty.App.Repositories
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly AnalogCoreV2 _client;

        public VoucherRepository(AnalogCoreV2 client)
        {
            _client = client;
        }
        
        public async Task<Try<ICollection<IssueVoucherResponse>>> IssueAsync(int amount, int productId, string description, string reqeuster, string prefix)
        {
            var request = new IssueVoucherRequest()
            {
                Amount = amount,
                Description = description,
                ProductId = productId,
                Requester = reqeuster,
                VoucherPrefix = prefix,
            };

            return await TryAsync(async () => await _client.ApiV2VouchersIssueVouchersAsync(request));
        }
    }
}