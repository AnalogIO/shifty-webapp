using Components;
using Shifty.Api.Generated.AnalogCoreV2;

namespace Shifty.App.DomainModels
{
    public class UnusedTicket
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int TicketsLeft { get; set; }
        public decimal UnusedPurchasesValue { get; set; }
        
        public static UnusedTicket FromDto(UnusedClipsResponse ticket)
        {
            return new UnusedTicket()
            {
                ProductId = ticket.ProductId,
                ProductName = ticket.ProductName,
                TicketsLeft = ticket.TicketsLeft,
                UnusedPurchasesValue = ticket.UnusedPurchasesValue,
            };
        }
    }
}