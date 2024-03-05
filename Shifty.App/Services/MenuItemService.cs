using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Components;
using LanguageExt;
using LanguageExt.Common;
using LanguageExt.UnsafeValueAccess;
using Shifty.Api.Generated.AnalogCoreV1;
using Shifty.Api.Generated.AnalogCoreV2;
using Shifty.App.DomainModels;
using Shifty.App.Repositories;

namespace Shifty.App.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenuItemRepository _MenuItemRepository;
        
        public MenuItemService(IMenuItemRepository MenuItemRepository)
        {
            _MenuItemRepository = MenuItemRepository;
        }
        
        public async Task<Try<MenuItem>> UpdateMenuItem(UpdateMenuItemRequest MenuItem, int id)
        {
            return await _MenuItemRepository
                        .UpdateMenuItem(MenuItem, id)
                        .Map(DomainModels.MenuItem.FromDto);
        }

        public async Task<Try<MenuItem>> AddMenuItem(AddMenuItemRequest MenuItem)
        {
            return await _MenuItemRepository
                        .AddMenuItem(MenuItem)
                        .Map(DomainModels.MenuItem.FromDto);
        }
        
        public async Task<Try<IEnumerable<MenuItem>>> GetMenuItems()
        {
            var temp = _MenuItemRepository
                        .GetMenuItems();

            return await temp.Map(x => 
                x.Map(MenuItem.FromDto));
        }
    }
}