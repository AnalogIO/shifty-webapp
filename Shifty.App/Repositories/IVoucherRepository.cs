using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.Common;
using Shifty.Api.Generated.AnalogCoreV2;

namespace Shifty.App.Repositories
{
    public interface IVoucherRepository
    {
        public Task<Either<Error, System.Collections.Generic.ICollection<IssueVoucherResponse>>> IssueAsync(int amount, int productId, string description);
    }
}