using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using LanguageExt;
using LanguageExt.Common;
using LanguageExt.UnsafeValueAccess;
using Shifty.Api.Generated.AnalogCoreV1;
using Shifty.App.Authentication;
using Shifty.App.Repositories;

namespace Shifty.App.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Try<IEnumerable<ProductDto>>> GetProducts()
        {
            return await _productRepository.GetProducts();
        }
    }
}