using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.Common;
using Shifty.Api.Generated.AnalogCoreV1;

namespace Shifty.App.Repositories
{
    public interface IAccountRepository
    {
        public Task<Either<Error, TokenDto>> LoginAsync(string username, string password);
    }
}