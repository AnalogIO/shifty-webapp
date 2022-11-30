using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shifty.Api.Generated.ShiftPlanningV1;
using LanguageExt;
using LanguageExt.Common;

namespace Shifty.App.Repositories;

public interface IEmployeeRepository
{
    Task<Either<Error, ICollection<EmployeeDTO>>> GetEmployees();
}