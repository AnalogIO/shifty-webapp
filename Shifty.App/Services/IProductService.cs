using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.Common;
using Shifty.Api.Generated.AnalogCoreV1;

namespace Shifty.App.Services
{
    public interface IProductService
    {
        /// <summary>
        /// Gives products available to user
        /// </summary>
        /// <returns>Collection of products in the form of ProductDtos. The Collection is null if an error happens.</returns>
        Task<Try<IEnumerable<ProductDto>>> GetProducts();
    }
}