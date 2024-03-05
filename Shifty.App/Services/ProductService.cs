using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt;
using Shifty.App.DomainModels;
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

        public async Task<Try<IEnumerable<Product>>> GetProducts()
        {
            return await _productRepository
                            .GetProducts()
                            .Map(x => x.Map(p => Product.FromDto(p)));
        }
        
        public async Task<Try<Product>> UpdateProduct(Product product, int id)
        {
            var productrequest = Product.ToUpdateRequest(product);

            return await _productRepository.UpdateProduct(id, productrequest)
                            .Map(p => Product.FromDto(p));
        }

        public async Task<Try<Product>> AddProduct(Product product)
        {
            var newproduct = await _productRepository.AddProduct(Product.ToAddRequest(product));

            return newproduct.Map(p => Product.FromDto(p));
        }
    }
}