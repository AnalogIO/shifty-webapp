using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.Common;
using Shifty.Api.Generated.ShiftPlanningV1;
using static LanguageExt.Prelude;

namespace Shifty.App.Repositories
{
    public class ShiftRepository : IShiftRepository
    {
        private readonly ShiftPlanningV1 _client;

        public ShiftRepository(ShiftPlanningV1 client)
        {
            _client = client;
        }
        
        public async Task<Either<Error, bool>> CheckOut(int shiftId, int employeeId)
        {
            return await TryAsync(_client.ApiShiftsCheckoutAsync(shiftId, employeeId))
                    .Map(_ => true).ToEither();
        }

        public async Task<Either<Error, bool>> CheckIn(int shiftId, int employeeId)
        {
            return await TryAsync(_client.ApiShiftsCheckinAsync(shiftId, employeeId))
                .Map(_ => true).ToEither();
        }

        public async Task<Either<Error, ICollection<ShiftDTO>>> TodayShifts()
        {
            return await TryAsync(_client.ApiShiftsTodayGetAsync()).ToEither();
        }

        public async Task<Either<Error, Task>> PatchShift(int shiftId, PatchShiftDTO patch)
        {
            return await TryAsync(_client.ApiShiftsPatchAsync(shiftId, patch)).ToEither();
        }
    }
}