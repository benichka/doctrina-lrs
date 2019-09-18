using Doctrina.Application.Interfaces;

namespace Doctrina.ExperienceApi.LRS.Authentication
{
    public class RequestAuthority : IRequestAuthority
    {
        public Agent Authority { get; set; }
    }
}