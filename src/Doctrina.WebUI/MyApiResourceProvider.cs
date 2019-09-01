using IdentityModel;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctrina.WebUI
{
    public static class MyApiResourceProvider
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                // Add a resource for some set of APIs that we may be protecting
                // Note that the constructor will automatically create an allowed scope with
                // name and claims equal to the resource's name and claims. If the resource
                // has different scopes/levels of access, the scopes property can be set to
                // list specific scopes included in this resource, instead.
                new ApiResource(
                    "myAPIs",                                       // Api resource name
                    "My API Set #1",                                // Display name
                    new[] { JwtClaimTypes.Name, JwtClaimTypes.Role, "office" }) // Claims to be included in access token
            };
        }
    }
}
