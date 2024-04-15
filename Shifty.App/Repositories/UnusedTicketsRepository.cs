using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguageExt;
using Shifty.Api.Generated.AnalogCoreV2;
using static LanguageExt.Prelude;

namespace Shifty.App.Repositories
{
    public class UnusedTicketRepository : IUnusedTicketRepository
    {
        private readonly AnalogCoreV2 _client;

        public UnusedTicketRepository(AnalogCoreV2 client)
        {
            _client = client;
        }

        public async Task<Try<IEnumerable<UnusedClipsResponse>>> GetTickets(UnusedClipsRequest unusedClipsRequest)
        {
            return await TryAsync(async () => (await _client.ApiV2StatisticsUnusedClipsAsync(unusedClipsRequest)).AsEnumerable());
        }
    }
}