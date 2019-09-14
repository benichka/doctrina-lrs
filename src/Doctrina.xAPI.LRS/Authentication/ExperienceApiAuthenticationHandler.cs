using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace Doctrina.xAPI.Store.Authentication
{
    public class ExperienceApiAuthenticationHandler : AuthenticationHandler<ExperienceApiAuthenticationOptions>
    {
        public ExperienceApiAuthenticationHandler(
            IOptionsMonitor<ExperienceApiAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Create authenticated user
            var identities = new List<ClaimsIdentity> { new ClaimsIdentity(AuthenticationTypes.Basic) };
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identities), ExperienceApiAuthenticationOptions.DefaultScheme);

            await Task.CompletedTask;
            return AuthenticateResult.Success(ticket);
        }
    }
}