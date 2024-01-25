using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.Common;
using Shifty.Api.Generated.AnalogCoreV1;
using Shifty.Api.Generated.AnalogCoreV2;
using Shifty.App.DomainModels;

namespace Shifty.App.Services
{
    public interface IProductService
    {
        /// <summary>
        /// Gives products available to user
        /// </summary>
        /// <returns>Collection of products in the form of ProductDtos. The Collection is null if an error happens.</returns>
        Task<Try<IEnumerable<Product>>> GetProducts();

        /// <summary>
        /// Updates a product
        /// </summary>
        /// <param name="product">The product request containing the updated product information</param>
        /// <returns>The updated product</returns>
        Task<Try<Product>> UpdateProduct(Product product, int id);

        /// <summary>
        /// Adds a product
        /// </summary>
        /// <param name="product">The product request containing the new product information</param>
        /// <returns>The newly created product</returns>
        Task<Try<Product>> AddProduct(Product product);
    }
}