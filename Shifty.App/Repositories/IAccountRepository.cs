using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.Common;
using Shifty.Api.Generated.AnalogCoreV1;
using Shifty.Api.Generated.AnalogCoreV2;

namespace Shifty.App.Repositories
{
    public interface IAccountRepository
    {
        public Task<Either<Error, TokenDto>> LoginAsync(string username, string password);
        public Task<Try<ICollection<UserSearchResponse>>> SearchUserAsync(string query, int page, int pageSize);
        public Task<Try<Task>> UpdateUserGroupAsync(int userId, UserGroup group);
    }
}