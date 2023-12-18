using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt;
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

    public async Task<Either<Error, TokenDto>> LoginAsync(string username, string password)
        {
            var dto = new LoginDto()
            {
                Email= username,
                Password = password,
                Version = "2.1.0"
            };
            return await TryAsync(_v1client.ApiV1AccountLoginAsync(loginDto: dto)).ToEither();
        }

        public async Task<Try<ICollection<UserSearchResponse>>> SearchUserAsync(string query, int page, int pageSize)
        {
            return await TryAsync(_v2client.ApiV2AccountSearchAsync(query, page, pageSize));
        }

        public async Task<Try<Task>> UpdateUserGroupAsync(int userId, UserGroup group)
        {
            return await TryAsync(_v2client.ApiV2AccountUserGroupAsync(userId, new(){UserGroup = group}));
        }
    }
}