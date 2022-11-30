using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using Shifty.Api.Generated.ShiftPlanningV1;

namespace Shifty.App.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ShiftPlanningV1 _client;

    public EmployeeRepository(ShiftPlanningV1 client)
    {
        _client = client;
    }

    public async Task<Either<Error, ICollection<EmployeeDTO>>> GetEmployees()
    {
        return await TryAsync(_client.ApiEmployeesGetAsync()).ToEither();
    }
}