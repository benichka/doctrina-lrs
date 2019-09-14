using Microsoft.AspNetCore.Authentication;

namespace Doctrina.xAPI.Store.Authentication
{
    public class ExperienceApiAuthenticationOptions : AuthenticationSchemeOptions
    {
        public static string DefaultScheme { get; } = "Authority Scheme";

        public ExperienceApiAuthenticationOptions()
        {
        }

    }
}