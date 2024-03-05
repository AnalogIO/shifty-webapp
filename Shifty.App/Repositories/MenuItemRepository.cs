using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.Common;
using Shifty.Api.Generated.AnalogCoreV1;
using Shifty.Api.Generated.AnalogCoreV2;
using Shifty.App.Services;
using static LanguageExt.Prelude;

namespace Shifty.App.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly AnalogCoreV2 _client;

        public MenuItemRepository(AnalogCoreV2 client)
        {
            _client = client;
        }

        public async Task<Try<IEnumerable<MenuItemResponse>>> GetMenuItems()
        {
            return await TryAsync(async () => (await _client.ApiV2MenuitemsGetAsync()).AsEnumerable());
        }

        public async Task<Try<MenuItemResponse>> UpdateMenuItem(UpdateMenuItemRequest menuItem, int menuItemId)
        {
            return await TryAsync(async () => await _client.ApiV2MenuitemsPutAsync(id: menuItemId, menuItem: menuItem));
        }

        public async Task<Try<MenuItemResponse>> AddMenuItem(AddMenuItemRequest menuItem)
        {
            return await TryAsync(async () => await _client.ApiV2MenuitemsPostAsync(menuItem));
        }
    }
}