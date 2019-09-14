using Doctrina.xAPI.Client.Http;
using Microsoft.AspNetCore.Mvc;

namespace Doctrina.xAPI.Store.Controllers
{
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
        public Agent Authority
        {
            get
            {
                if (User.Identity.IsAuthenticated)
                {
                    // TODO: The current authority should be resolved using middleware and scoped through application

                    return new Agent()
                    {
                        Name = User.Identity.Name,
                        OpenId = new Iri($"{Request.Scheme}://{Request.Host.Value}")
                    };
                }

                return null;
            }
        }

        public ApiVersion APIVersion
        {
            get
            {
                return Request.Headers[Headers.XExperienceApiVersion].ToString();
            }
        }
    }
}
