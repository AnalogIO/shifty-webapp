using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt;
using Shifty.Api.Generated.AnalogCoreV2;
using Shifty.App.Repositories;

namespace Shifty.App.Services;

public class TicketService(ITicketRepository repository) : ITicketService
{
    public async Task<Try<IEnumerable<UsedTicketEvent>>> GetRecentlyUsedTickets(int limit)
    {
        return await repository.GetRecentlyUsedTickets(limit);
    }
}