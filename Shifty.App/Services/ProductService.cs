using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using LanguageExt;
using LanguageExt.Common;
using LanguageExt.UnsafeValueAccess;
using Shifty.Api.Generated.AnalogCoreV1;
using Shifty.Api.Generated.AnalogCoreV2;
using Shifty.App.Authentication;
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
            var productrequest = new UpdateProductRequest(){
                        Id = id,
                        Name = product.Name,
                        Price = product.Price,
                        Description = product.Description,
                        AllowedUserGroups = product.AllowedUserGroups.ToList(),
                        MenuItemIds = product.EligibleMenuItems.Map(x => x.Id).ToList(),
                        NumberOfTickets = product.NumberOfTickets,
                        Visible = product.Visible
            };

            return await _productRepository.UpdateProduct(productrequest)
                            .Map(p => Product.FromChangedProduct(p, id, false)); // TODO: Fix when UpdateProduct returns IsPerk property
        }

        public async Task<Try<Product>> AddProduct(Product product)
        {
            var productrequest = new AddProductRequest(){
                        Name = product.Name,
                        Price = product.Price,
                        Description = product.Description,
                        AllowedUserGroups = product.AllowedUserGroups.ToList(),
                        MenuItemIds = product.EligibleMenuItems.Map(x => x.Id).ToList(),
                        NumberOfTickets = product.NumberOfTickets,
                        Visible = product.Visible
            };

            var changedproduct = await _productRepository.AddProduct(productrequest);

            return changedproduct.Map(p => Product.FromChangedProduct(p, 0, false)); // TODO: Fix when AddProduct no longer returns a ChangedProductResponse 
        }
    }
}