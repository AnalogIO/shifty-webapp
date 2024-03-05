using System.Collections.Generic;
using System.Linq;
using Shifty.Api.Generated.AnalogCoreV2;

namespace Shifty.App.DomainModels
{
    public class Product {
        public int Id { get; init; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; init; }
        public int NumberOfTickets { get; set; }
        public IEnumerable<MenuItem> EligibleMenuItems { get; set; }
        public IEnumerable<UserGroup> AllowedUserGroups { get; set; }
        public bool Visible { get; set; }
        public bool IsPerk { get; set; }
        public static Product FromDto(ProductResponse dto)
        {
            return new Product()
            {
                Id = dto.Id,
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description,
                NumberOfTickets = dto.NumberOfTickets,
                EligibleMenuItems = dto.EligibleMenuItems.Map(MenuItem.FromDto),
                AllowedUserGroups = dto.AllowedUserGroups,
                Visible = dto.Visible,
                IsPerk = dto.IsPerk
            };
        }

        public static AddProductRequest ToAddRequest(Product product)
        {
            return new AddProductRequest()
            {
                Name = product.Name,
                Description = product.Description,
                NumberOfTickets = product.NumberOfTickets,
                MenuItemIds = product.EligibleMenuItems.Map(x => x.Id).ToList(),
                AllowedUserGroups = product.AllowedUserGroups.ToList(),
                Visible = product.Visible,
                Price = product.Price,
            };
        }

        public static UpdateProductRequest ToUpdateRequest(Product product)
        {
            return new UpdateProductRequest()
            {
                Name = product.Name,
                Description = product.Description,
                NumberOfTickets = product.NumberOfTickets,
                MenuItemIds = product.EligibleMenuItems.Map(x => x.Id).ToList(),
                AllowedUserGroups = product.AllowedUserGroups.ToList(),
                Visible = product.Visible,
                Price = product.Price,
            };
        }
    }
}