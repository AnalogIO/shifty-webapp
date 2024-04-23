using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.Common;
using Shifty.Api.Generated.AnalogCoreV1;
using Shifty.Api.Generated.AnalogCoreV2;

namespace Shifty.App.Repositories
{
    public interface IUnusedTicketRepository
    {
        Task<Try<IEnumerable<UnusedClipsResponse>>> GetTickets(UnusedClipsRequest unusedClipsRequest);
    }
}