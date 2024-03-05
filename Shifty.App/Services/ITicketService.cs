using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt;
using Shifty.Api.Generated.AnalogCoreV2;

namespace Shifty.App.Services
{
    public interface ITicketService
    {
        Task<Try<IEnumerable<UsedTicketEvent>>> GetRecentlyUsedTickets(int limit);
    }
}