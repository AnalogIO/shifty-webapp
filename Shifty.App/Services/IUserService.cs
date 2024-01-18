using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.Common;
using Shifty.Api.Generated.AnalogCoreV2;

namespace Shifty.App.Services
{
    public interface IUserService
    {
        Task<Try<UserSearchResponse>> SearchUsers(string query, int pageNumber = 0, int pageSize = 10);
        Task<Try<Task>> UpdateUserGroupAsync(int userId, UserGroup group);
    }
}