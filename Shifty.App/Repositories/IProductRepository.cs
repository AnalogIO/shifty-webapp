using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.Common;
using Shifty.Api.Generated.AnalogCoreV1;
using Shifty.Api.Generated.AnalogCoreV2;

namespace Shifty.App.Repositories
{
    public interface IProductRepository
    {
        public Task<Try<System.Collections.Generic.IEnumerable<ProductResponse>>> GetProducts();
    }
}