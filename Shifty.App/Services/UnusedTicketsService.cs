using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt;
using Shifty.Api.Generated.AnalogCoreV2;
using Shifty.App.DomainModels;
using Shifty.App.Repositories;

namespace Shifty.App.Services
{
    public class UnusedTicketsService : IUnusedTicketsService
    {
        private readonly IUnusedTicketRepository _unusedTicketRepository;
        
        public UnusedTicketsService(IUnusedTicketRepository unusedTicketRepository)
        {
            _unusedTicketRepository = unusedTicketRepository;
        }

        public async Task<Try<IEnumerable<UnusedTicket>>> GetUnusedTickets(DateTimeOffset from, DateTimeOffset to)
        {
            return await _unusedTicketRepository
                            .GetTickets(new UnusedClipsRequest(){
                                StartDate = from,
                                EndDate = to
                            })
                            .Map(x => x.Map(UnusedTicket.FromDto));
        }
    }
}