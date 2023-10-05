using System.Collections.Generic;
using System.Threading.Tasks;
using Shifty.Api.Generated.AnalogCoreV1;

namespace Shifty.App.Services
{
    public interface IProductService
    {
        Task<(bool, ICollection<ProductDto>)> GetProducts();
    }
}