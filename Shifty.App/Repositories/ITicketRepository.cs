using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt;
using Shifty.Api.Generated.AnalogCoreV2;

namespace Shifty.App.Repositories;

public interface ITicketRepository
{
    Task<Try<IEnumerable<UsedTicketEvent>>> GetRecentlyUsedTickets(int limit);
}