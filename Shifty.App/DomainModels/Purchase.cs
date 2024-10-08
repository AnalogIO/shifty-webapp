using System;
using Microsoft.VisualBasic;
using Shifty.Api.Generated.AnalogCoreV1;
using Shifty.Api.Generated.AnalogCoreV2;

namespace Shifty.App.DomainModels
{
    public record Purchase {
        public int Id { get; init; }
        public int NumberOfTickets { get; set; }
        public int Price { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public PurchaseStatus PurchaseStatus { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public static Purchase FromDto(SimplePurchaseResponse dto)
        {
            return new Purchase()
            {
                Id = dto.Id,
                DateCreated = dto.DateCreated,
                NumberOfTickets = dto.NumberOfTickets,
                Price = dto.TotalAmount,
                PurchaseStatus = dto.PurchaseStatus,
                ProductName = dto.ProductName,
                ProductId = dto.ProductId
            };
        }

    }
}