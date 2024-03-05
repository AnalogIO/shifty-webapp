using Shifty.Api.Generated.AnalogCoreV2;

namespace Shifty.App.DomainModels
{
    public record MenuItem {
        public int Id { get; init; }
        public string Name { get; set; }
        public static MenuItem FromDto(MenuItemResponse dto)
        {
            return new MenuItem()
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }

        public static AddMenuItemRequest ToAddRequest(MenuItem menuItem)
        {
            return new AddMenuItemRequest()
            {
                Name = menuItem.Name
            };
        }

        public static UpdateMenuItemRequest ToUpdateRequest(MenuItem menuItem)
        {
            return new UpdateMenuItemRequest()
            {
                Name = menuItem.Name
            };
        }
    }
}