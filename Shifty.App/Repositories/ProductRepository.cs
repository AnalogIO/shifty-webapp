using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.Common;
using Shifty.Api.Generated.AnalogCoreV1;
using Shifty.App.Services;
using static LanguageExt.Prelude;

namespace Shifty.App.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AnalogCoreV1 _client;

        public ProductRepository(AnalogCoreV1 client)
        {
            _client = client;
        }
        
        async Task<Either<Error, ICollection<ProductDto>>> IProductRepository.GetProducts()
        {
            return await TryAsync(_client.ApiV1ProductsAsync()).ToEither();
        }
    }
}