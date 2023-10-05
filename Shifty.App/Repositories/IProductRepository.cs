using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.Common;
using Shifty.Api.Generated.AnalogCoreV1;

namespace Shifty.App.Repositories
{
    public interface IProductRepository
    {
        public Task<Either<Error, System.Collections.Generic.ICollection<ProductDto>>> GetProducts();
    }
}