using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.Common;
using Shifty.Api.Generated.AnalogCoreV1;
using static LanguageExt.Prelude;

namespace Shifty.App.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AnalogCoreV1 _client;

        public AccountRepository(AnalogCoreV1 client)
        {
            _client = client;
        }
        
        public async Task<Either<Error, TokenDto>> LoginAsync(string username, string password)
        {
            var dto = new LoginDto()
            {
                Email= username,
                Password = password,
                Version = "2.1.0"
            };
            return await TryAsync(_client.ApiV1AccountLoginAsync(loginDto: dto)).ToEither();
        }
    }
}