using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shifty.Api.Generated.AnalogCoreV1
{
    public partial class AnalogCoreV1
    {
        static partial void UpdateJsonSerializerSettings(JsonSerializerOptions settings)
        {
            settings.Converters.Add(new JsonStringEnumConverter());
        }
    }
}

namespace Shifty.Api.Generated.AnalogCoreV2
{
    public partial class AnalogCoreV2
    {
        static partial void UpdateJsonSerializerSettings(JsonSerializerOptions settings)
        {
            settings.Converters.Add(new JsonStringEnumConverter());
        }
    }
}
