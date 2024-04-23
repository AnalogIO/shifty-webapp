using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.Common;
using MudBlazor;
using Shifty.Api.Generated.AnalogCoreV1;
using Shifty.Api.Generated.AnalogCoreV2;
using Shifty.App.DomainModels;

namespace Shifty.App.Services
{
    public interface IUnusedTicketsService
    {
        /// <summary>
        /// Queries unused tickets
        /// </summary>
        /// <param name="from">The first date to retrieve unused tickets from</param>
        /// <param name="to">The last date to retrieve unused tickets to</param>
        /// <returns>A list of unused tickets grouped by product</returns>
        Task<Try<IEnumerable<UnusedTicket>>> GetUnusedTickets(DateTimeOffset from, DateTimeOffset to);
    }
}