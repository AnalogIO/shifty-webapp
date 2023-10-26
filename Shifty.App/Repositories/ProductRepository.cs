using System;
using System.Collections.Generic;
using System.Linq;
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
        
        async Task<Try<IEnumerable<ProductDto>>> IProductRepository.GetProducts()
        {
            return await TryAsync(async () => (await _client.ApiV1ProductsAsync()).AsEnumerable());
        }
    }
}