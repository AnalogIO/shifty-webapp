using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguageExt;
using static LanguageExt.Prelude;
using Shifty.Api.Generated.AnalogCoreV2;

namespace Shifty.App.Repositories;

public class TicketRepository(AnalogCoreV2 client) : ITicketRepository
{
    public async Task<Try<IEnumerable<UsedTicketEvent>>> GetRecentlyUsedTickets(int limit)
    {
        return await TryAsync(async () => (await client.ApiV2TicketsRecentAsync(limit)).AsEnumerable());
    }
}