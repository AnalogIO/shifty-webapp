using System.Collections.Generic;
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
        
        public async Task<Either<Error, ICollection<IssueVoucherResponse>>> IssueAsync(int amount, int productId, string description)
        {
            var account = await _client.ApiV2AccountGetAsync();
            var request = new IssueVoucherRequest()
            {
                Amount = amount,
                Description = description,
                ProductId = productId,
                Requester = account.Name,
                VoucherPrefix = account.Email[..3],
            };
            return await TryAsync(_client.ApiV2VouchersIssueVouchersAsync(request)).ToEither();
        }
    }
}