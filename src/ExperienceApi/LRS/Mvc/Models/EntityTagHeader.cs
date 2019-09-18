using Doctrina.ExperienceApi.Client.Http;

namespace Doctrina.ExperienceApi.LRS.Mvc.Models
{
    public class EntityTagHeader
    {
        public string Tag { get; internal set; }
        public ETagMatch Match { get; internal set; }
    }
}
