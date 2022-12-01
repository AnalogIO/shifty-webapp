using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.Common;
using Shifty.Api.Generated.ShiftPlanningV1;


namespace Shifty.App.Repositories
{
    public interface IShiftRepository
    {
        Task<Either<Error, bool>> CheckOut(int shiftId, int employeeId);
        Task<Either<Error, bool>> CheckIn(int shiftId, int employeeId);
        Task<Either<Error, ICollection<ShiftDTO>>> TodayShifts();
        Task<Either<Error, Task>> PatchShift(int shiftId, PatchShiftDTO patch);
    }
}