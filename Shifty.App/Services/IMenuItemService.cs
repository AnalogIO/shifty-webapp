using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.Common;
using Shifty.Api.Generated.AnalogCoreV1;
using Shifty.Api.Generated.AnalogCoreV2;
using Shifty.App.DomainModels;

namespace Shifty.App.Services
{
    public interface IMenuItemService
    {
        /// <summary>
        /// Gives MenuItems available to user
        /// </summary>
        /// <returns>Collection of MenuItems in the form of MenuItemDtos. The Collection is null if an error happens.</returns>
        Task<Try<IEnumerable<MenuItem>>> GetMenuItems();

        /// <summary>
        /// Updates a MenuItem
        /// </summary>
        /// <param name="MenuItem">The MenuItem request containing the updated MenuItem information</param>
        /// <param name="id">The id of the MenuItem to update</param>
        /// <returns>The updated MenuItem</returns>
        Task<Try<MenuItem>> UpdateMenuItem(UpdateMenuItemRequest MenuItem, int id);

        /// <summary>
        /// Adds a MenuItem
        /// </summary>
        /// <param name="MenuItem">The MenuItem request containing the new MenuItem information</param>
        /// <returns>The newly created MenuItem</returns>
        Task<Try<MenuItem>> AddMenuItem(AddMenuItemRequest MenuItem);
    }
}