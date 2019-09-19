using Newtonsoft.Json.Linq;

namespace Doctrina.ExperienceApi.Data.Json
{
    public class JsonTokenModelException : JsonModelException
    {
        public JsonTokenModelException(JToken token, string message)
            : base($"{message.EnsureEndsWith(".")} Path: '{token.Path}'")
        {
            Token = token;
        }

        public JToken Token { get; }
    }
}
