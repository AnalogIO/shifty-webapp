using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.ClassInstances.Pred;
using LanguageExt.Common;
using Shifty.Api.Generated.AnalogCoreV1;
using Shifty.Api.Generated.AnalogCoreV2;
using static LanguageExt.Prelude;

namespace Shifty.App.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AnalogCoreV1 _v1client;
        private readonly AnalogCoreV2 _v2client;

        public AccountRepository(AnalogCoreV1 v1client, AnalogCoreV2 v2client)
        {
            _v1client = v1client;
            _v2client = v2client;
        }

    public async Task<Either<Error, Task>> LoginAsync(string username, string password)
        {
            var dto = new UserLoginRequest(){
                Email = username
            };

            return await TryAsync(_v2client.ApiV2AccountLoginAsync(dto)).ToEither();
        }

        public async Task<Try<UserSearchResponse>> SearchUserAsync(string query, int page, int pageSize)
        {
            return await TryAsync(_v2client.ApiV2AccountSearchAsync(filter: query, pageNum: page, pageLength: pageSize));
        }

        public async Task<Try<Task>> UpdateUserGroupAsync(int userId, UserGroup group)
        {
            return await TryAsync(_v2client.ApiV2AccountUserGroupAsync(userId, new(){UserGroup = group}));
        }

        public async Task<UserLoginResponse> AuthenticateAsync(string token)
        {
            return await _v2client.ApiV2AccountAuthLoginAsync(token, LoginType.Shifty);
        }

        public async Task<Try<UserLoginResponse>> RefreshTokenAsync()
        {
            return await TryAsync(_v2client.ApiV2AccountAuthRefreshAsync(LoginType.Shifty, null));
        }
    }
}