using System.Collections.Generic;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using LanguageExt;
using LanguageExt.Common;
using Shifty.Api.Generated.AnalogCoreV2;
using Shifty.App.Repositories;

namespace Shifty.App.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountRepository _accountRepository;
        
        public UserService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Task<Try<ICollection<UserSearchResponse>>> SearchUsers(string query, int pageNumber = 0, int pageSize = 10)
        {
            return _accountRepository.SearchUserAsync(query, pageNumber, pageSize);
        }

        public Task<Try<Task>> UpdateUserGroupAsync(int userId, UserGroup group)
        {
            return _accountRepository.UpdateUserGroupAsync(userId, group);  
        }
    }
}