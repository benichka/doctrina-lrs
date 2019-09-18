using Newtonsoft.Json.Linq;

namespace Doctrina.ExperienceApi
{
    public interface IStatementObject
    {
        ObjectType ObjectType { get; }

        JToken ToJToken(ApiVersion version, ResultFormat format);
    }
}
