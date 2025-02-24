using Shifty.Api.Generated.AnalogCoreV2;

namespace Shifty.App.DomainModels
{
    public class MenuItem {
        public int Id { get; init; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public static MenuItem FromDto(MenuItemResponse dto)
        {
            return new MenuItem()
            {
                Id = dto.Id,
                Name = dto.Name,
                Active = dto.Active
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
                Name = menuItem.Name,
                Active = menuItem.Active
            };
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            return obj is MenuItem menuItem &&
                   Id == menuItem.Id;
        }
        
        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}